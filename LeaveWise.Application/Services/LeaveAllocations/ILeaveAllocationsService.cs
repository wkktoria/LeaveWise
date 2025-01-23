using LeaveWise.Application.Models.LeaveAllocations;
using LeaveWise.Data;

namespace LeaveWise.Application.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeaveAsync(string employeeId);

    // Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId);

    Task<EmployeeAllocationVm> GetEmployeeAllocationsAsync(string? userId);

    Task<List<EmployeeListVm>> GetEmployeesAsync();

    Task<LeaveAllocationEditVm> GetEmployeeAllocationAsync(int allocationId);

    Task EditAllocationAsync(LeaveAllocationEditVm allocationEditVm);
    
    Task<LeaveAllocation> GetCurrantAllocationAsync(int leaveTypeId, string employeeId);
}