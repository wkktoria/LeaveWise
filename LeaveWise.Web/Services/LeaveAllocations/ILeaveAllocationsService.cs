using LeaveWise.Web.Models.LeaveAllocations;

namespace LeaveWise.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeaveAsync(string employeeId);

    // Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId);

    Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId);

    Task<List<EmployeeListVM>> GetEmployeesAsync();

    Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId);

    Task EditAllocationAsync(LeaveAllocationEditVM allocationEditVm);
}