using LeaveWise.Web.Models.LeaveRequests;

namespace LeaveWise.Web.Services.LeaveRequests;

public interface ILeaveRequestsService
{
    Task CreateLeaveRequestAsync(LeaveRequestCreateVM model);

    Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequestsAsync();

    Task<LeaveRequestListVM> GetAllLeaveRequestsAsync();
    
    Task CancelLeaveRequestAsync(int leaveRequestId);
    
    Task ReviewLeaveRequestAsync(int leaveRequestId, bool approved);
    
    Task<bool> RequestDatesExceedAllocationAsync(LeaveRequestCreateVM model);

    Task<ReviewLeaveRequestVM> GetLeaveRequestForReviewAsync(int id);
}