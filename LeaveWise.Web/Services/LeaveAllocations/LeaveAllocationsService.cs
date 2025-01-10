using AutoMapper;
using LeaveWise.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveWise.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(
    ApplicationDbContext context,
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager,
    IMapper mapper)
    : ILeaveAllocationsService
{
    public async Task AllocateLeaveAsync(string employeeId)
    {
        var leaveTypes = await context.LeaveTypes
            .Where(t => t.LeaveAllocations!.All(a => a.EmployeeId != employeeId))
            .ToListAsync();

        var currentDate = DateTime.Now;
        var period = await context.Periods
            .SingleAsync(p => p.EndDate.Year == currentDate.Year);
        var monthsRemaining = period.EndDate.Month - currentDate.Month;

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

    public async Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId)
    {
        var user = string.IsNullOrWhiteSpace(userId)
            ? await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User!)
            : await userManager.FindByIdAsync(userId);
        var allocations = await GetAllocationsAsync(userId);
        var allocationVmList = mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
        var leaveTypesCount = await context.LeaveTypes.CountAsync();

        var employeeVm = new EmployeeAllocationVM
        {
            DateOfBirth = user!.DateOfBirth,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVmList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count
        };

        return employeeVm;
    }

    public async Task<List<EmployeeListVM>> GetEmployeesAsync()
    {
        var users = await userManager.GetUsersInRoleAsync(Roles.Employee);
        var employees = mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

        return employees;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId)
    {
        var allocation = await context.LeaveAllocations.Include(a => a.LeaveType)
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == allocationId);
        var model = mapper.Map<LeaveAllocationEditVM>(allocation);

        return model;
    }

    public async Task EditAllocationAsync(LeaveAllocationEditVM allocationEditVm)
    {
        await context.LeaveAllocations
            .Where(a => a.Id == allocationEditVm.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.Days, allocationEditVm.Days));
    }

    private async Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId)
    {
        var currentDate = DateTime.Now;

        var leaveAllocations = await context.LeaveAllocations
            .Include(a => a.LeaveType)
            .Include(a => a.Period)
            .Where(a => a.EmployeeId == userId && a.Period!.EndDate.Year == currentDate.Year)
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