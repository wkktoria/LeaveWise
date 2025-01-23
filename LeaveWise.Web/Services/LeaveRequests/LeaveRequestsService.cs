using AutoMapper;
using LeaveWise.Web.Models.LeaveAllocations;
using LeaveWise.Web.Models.LeaveRequests;
using LeaveWise.Web.Services.LeaveAllocations;
using LeaveWise.Web.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveWise.Web.Services.LeaveRequests;

public class LeaveRequestsService(
    ApplicationDbContext context,
    IMapper mapper,
    ILeaveAllocationsService leaveAllocationsService,
    IUsersService usersService) : ILeaveRequestsService
{
    public async Task CreateLeaveRequestAsync(LeaveRequestCreateVM model)
    {
        var leaveRequest = mapper.Map<LeaveRequest>(model);
        var user = await usersService.GetLoggedInUserAsync();

        leaveRequest.EmployeeId = user.Id;
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

        context.LeaveRequests.Add(leaveRequest);

        await UpdateAllocationDays(leaveRequest, true);
        await context.SaveChangesAsync();
    }

    public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequestsAsync()
    {
        var user = await usersService.GetLoggedInUserAsync();
        var leaveRequests = await context.LeaveRequests
            .Include(r => r.LeaveType)
            .Where(r => r.EmployeeId == user.Id)
            .ToListAsync();
        var model = leaveRequests.Select(r => new LeaveRequestReadOnlyVM
        {
            Id = r.Id,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            LeaveType = r.LeaveType?.Name!,
            LeaveRequestStatus = (LeaveRequestStatusEnum)r.LeaveRequestStatusId,
            NumberOfDays = r.EndDate.DayNumber - r.StartDate.DayNumber,
        }).ToList();

        return model;
    }

    public async Task<LeaveRequestListVM> GetAllLeaveRequestsAsync()
    {
        var leaveRequests = await context.LeaveRequests
            .Include(r => r.LeaveType)
            .ToListAsync();

        var approvedLeaveRequestsCount =
            leaveRequests.Count(r => r.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved);
        var pendingLeaveRequestsCount =
            leaveRequests.Count(r => r.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending);
        var declinedLeaveRequestsCount =
            leaveRequests.Count(r => r.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined);

        var leaveRequestsModels = leaveRequests.Select(r => new LeaveRequestReadOnlyVM()
        {
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Id = r.Id,
            LeaveType = r.LeaveType!.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)r.LeaveRequestStatusId,
            NumberOfDays = r.EndDate.DayNumber - r.StartDate.DayNumber,
        }).ToList();

        var model = new LeaveRequestListVM
        {
            ApprovedRequests = approvedLeaveRequestsCount,
            PendingRequests = pendingLeaveRequestsCount,
            DeclinedRequests = declinedLeaveRequestsCount,
            TotalRequests = leaveRequests.Count,
            LeaveRequests = leaveRequestsModels,
        };

        return model;
    }

    public async Task CancelLeaveRequestAsync(int leaveRequestId)
    {
        var leaveRequest = await context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest!.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;

        await UpdateAllocationDays(leaveRequest, false);
        await context.SaveChangesAsync();
    }

    public async Task ReviewLeaveRequestAsync(int leaveRequestId, bool approved)
    {
        var user = await usersService.GetLoggedInUserAsync();
        var leaveRequest = await context.LeaveRequests.FindAsync(leaveRequestId);

        leaveRequest!.LeaveRequestStatusId =
            approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;
        leaveRequest.ReviewerId = user.Id;

        if (!approved)
        {
            await UpdateAllocationDays(leaveRequest, false);
        }

        await context.SaveChangesAsync();
    }

    public async Task<bool> RequestDatesExceedAllocationAsync(LeaveRequestCreateVM model)
    {
        var user = await usersService.GetLoggedInUserAsync();
        var currentDate = DateTime.Now;
        var period = await context.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var allocation = await context.LeaveAllocations
            .FirstAsync(a => a.LeaveTypeId == model.LeaveTypeId && a.EmployeeId == user.Id && a.PeriodId == period.Id);

        return allocation.Days < numberOfDays;
    }

    public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReviewAsync(int id)
    {
        var leaveRequest = await context.LeaveRequests
            .Include(r => r.LeaveType)
            .FirstAsync(r => r.Id == id);
        var user = await usersService.GetUserByIdAsync(leaveRequest.EmployeeId);

        var model = new ReviewLeaveRequestVM
        {
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
            Id = leaveRequest.Id,
            LeaveType = leaveRequest.LeaveType!.Name,
            RequestComments = leaveRequest.RequestComments!,
            Employee = new EmployeeListVM
            {
                Id = leaveRequest.EmployeeId,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
            }
        };

        return model;
    }

    private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
    {
        var allocation =
            await leaveAllocationsService.GetCurrantAllocationAsync(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
        var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

        if (deductDays)
        {
            allocation.Days -= numberOfDays;
        }
        else
        {
            allocation.Days += numberOfDays;
        }

        context.Entry(allocation).State = EntityState.Modified;
    }

    private static int CalculateDays(DateOnly start, DateOnly end) => end.DayNumber - start.DayNumber;
}