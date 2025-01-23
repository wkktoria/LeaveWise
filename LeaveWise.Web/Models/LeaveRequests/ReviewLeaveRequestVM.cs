using System.ComponentModel;
using LeaveWise.Web.Models.LeaveAllocations;

namespace LeaveWise.Web.Models.LeaveRequests;

public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
{
    public EmployeeListVM Employee { get; set; } = new();
    
    [DisplayName("Additional Information")]
    public string RequestComments { get; set; } = string.Empty;
}