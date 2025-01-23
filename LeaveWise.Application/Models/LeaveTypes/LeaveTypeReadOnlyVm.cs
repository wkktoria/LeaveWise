using System.ComponentModel.DataAnnotations;

namespace LeaveWise.Application.Models.LeaveTypes;

public class LeaveTypeReadOnlyVm : BaseLeaveTypeVm
{
    public string Name { get; set; } = string.Empty;
    
    [Display(Name = "Maximum Allocation of Days")]
    public int NumberOfDays { get; set; }
}