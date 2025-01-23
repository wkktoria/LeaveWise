using System.ComponentModel.DataAnnotations;

namespace LeaveWise.Application.Models.LeaveRequests;

public class LeaveRequestListVm
{
    [Display(Name = "Total number of Requests")]
    public int TotalRequests { get; set; }

    [Display(Name = "Approved Requests")] public int ApprovedRequests { get; set; }

    [Display(Name = "Pending Requests")] public int PendingRequests { get; set; }

    [Display(Name = "Rejected Requests")] public int DeclinedRequests { get; set; }

    public List<LeaveRequestReadOnlyVm> LeaveRequests { get; set; } = [];
}