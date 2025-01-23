using System.ComponentModel;
using LeaveWise.Application.Models.LeaveAllocations;

namespace LeaveWise.Application.Models.LeaveRequests;

public class ReviewLeaveRequestVm : LeaveRequestReadOnlyVm
{
    public EmployeeListVm Employee { get; set; } = new();
    
    [DisplayName("Additional Information")]
    public string RequestComments { get; set; } = string.Empty;
}