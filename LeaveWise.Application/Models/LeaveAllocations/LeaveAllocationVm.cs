using LeaveWise.Application.Models.Periods;
using LeaveWise.Application.Models.LeaveTypes;

namespace LeaveWise.Application.Models.LeaveAllocations;

public class LeaveAllocationVm
{
    public int Id { get; set; }

    [Display(Name = "Number of Days")] public int Days { get; set; }

    [Display(Name = "Allocation Period")] public PeriodVM Period { get; set; } = new();

    public LeaveTypeReadOnlyVm LeaveType { get; set; } = new();
}