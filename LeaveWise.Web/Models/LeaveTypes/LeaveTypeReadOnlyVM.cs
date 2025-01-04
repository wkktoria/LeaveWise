using System.ComponentModel.DataAnnotations;

namespace LeaveWise.Web.Models.LeaveTypes;

public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM
{
    public string Name { get; set; } = string.Empty;
    
    [Display(Name = "Maximum Allocation of Days")]
    public int NumberOfDays { get; set; }
}