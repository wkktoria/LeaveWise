using AutoMapper;
using LeaveWise.Application.Models.LeaveAllocations;
using LeaveWise.Application.Services.Periods;
using LeaveWise.Application.Services.Users;
using LeaveWise.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveWise.Application.Services.LeaveAllocations;

public class LeaveAllocationsService(
    ApplicationDbContext context,
    IMapper mapper,
    IPeriodsService periodsService,
    IUsersService usersService)
    : ILeaveAllocationsService
{
    public async Task AllocateLeaveAsync(string employeeId)
    {
        var leaveTypes = await context.LeaveTypes
            .Where(t => t.LeaveAllocations!.All(a => a.EmployeeId != employeeId))
            .ToListAsync();

        var period = await periodsService.GetCurrentPeriodAsync();
        var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

        foreach (var leaveType in leaveTypes)
        {
            var accrualRate = decimal.Divide(leaveType.NumberOfDays, 12);

            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(accrualRate * monthsRemaining)
            };

            await context.AddAsync(leaveAllocation);
        }

        await context.SaveChangesAsync();
    }

    public async Task<EmployeeAllocationVm> GetEmployeeAllocationsAsync(string? userId)
    {
        var user = string.IsNullOrWhiteSpace(userId)
            ? await usersService.GetLoggedInUserAsync()
            : await usersService.GetUserByIdAsync(userId);
        var allocations = await GetAllocationsAsync(user.Id);
        var allocationVmList = mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVm>>(allocations);
        var leaveTypesCount = await context.LeaveTypes.CountAsync();

        var employeeVm = new EmployeeAllocationVm
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVmList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count
        };

        return employeeVm;
    }

    public async Task<List<EmployeeListVm>> GetEmployeesAsync()
    {
        var users = await usersService.GetEmployeesAsync();
        var employees = mapper.Map<List<ApplicationUser>, List<EmployeeListVm>>(users.ToList());

        return employees;
    }

    public async Task<LeaveAllocationEditVm> GetEmployeeAllocationAsync(int allocationId)
    {
        var allocation = await context.LeaveAllocations.Include(a => a.LeaveType)
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == allocationId);
        var model = mapper.Map<LeaveAllocationEditVm>(allocation);

        return model;
    }

    public async Task EditAllocationAsync(LeaveAllocationEditVm allocationEditVm)
    {
        await context.LeaveAllocations
            .Where(a => a.Id == allocationEditVm.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.Days, allocationEditVm.Days));
    }

    public async Task<LeaveAllocation> GetCurrantAllocationAsync(int leaveTypeId, string employeeId)
    {
        var period = await periodsService.GetCurrentPeriodAsync();
        var allocation = await context.LeaveAllocations
            .FirstAsync(a => a.LeaveTypeId == leaveTypeId && a.EmployeeId == employeeId && a.PeriodId == period.Id);
        return allocation;
    }

    private async Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId)
    {
        var period = await periodsService.GetCurrentPeriodAsync();
        var leaveAllocations = await context.LeaveAllocations
            .Include(a => a.LeaveType)
            .Include(a => a.Period)
            .Where(a => a.EmployeeId == userId && a.PeriodId == period.Id)
            .ToListAsync();

        return leaveAllocations;
    }

    private async Task<bool> AllocationExistsAsync(string userId, int periodId, int leaveTypeId)
    {
        var exists = await context.LeaveAllocations.AnyAsync(a => a.EmployeeId == userId
                                                                  && a.PeriodId == periodId &&
                                                                  a.LeaveTypeId == leaveTypeId);
        return exists;
    }
}