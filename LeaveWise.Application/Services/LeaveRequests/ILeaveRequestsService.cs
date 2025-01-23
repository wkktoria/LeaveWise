using LeaveWise.Application.Models.LeaveRequests;

namespace LeaveWise.Application.Services.LeaveRequests;

public interface ILeaveRequestsService
{
    Task CreateLeaveRequestAsync(LeaveRequestCreateVm model);

    Task<List<LeaveRequestReadOnlyVm>> GetEmployeeLeaveRequestsAsync();

    Task<LeaveRequestListVm> GetAllLeaveRequestsAsync();
    
    Task CancelLeaveRequestAsync(int leaveRequestId);
    
    Task ReviewLeaveRequestAsync(int leaveRequestId, bool approved);
    
    Task<bool> RequestDatesExceedAllocationAsync(LeaveRequestCreateVm model);

    Task<ReviewLeaveRequestVm> GetLeaveRequestForReviewAsync(int id);
}