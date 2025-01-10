using LeaveWise.Web.Models.LeaveTypes;
using LeaveWise.Web.Models.Periods;

namespace LeaveWise.Web.Models.LeaveAllocations;

public class LeaveAllocationVM
{
    public int Id { get; set; }

    [Display(Name = "Number of Days")] public int Days { get; set; }

    [Display(Name = "Allocation Period")] public PeriodVM Period { get; set; } = new();

    public LeaveTypeReadOnlyVM LeaveType { get; set; } = new();
}