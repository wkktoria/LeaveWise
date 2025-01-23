using System.ComponentModel.DataAnnotations;
using LeaveWise.Application.Models.LeaveAllocations;

namespace LeaveWise.Application.Models.LeaveAllocations;

public class EmployeeAllocationVm : EmployeeListVm
{
    [Display(Name = "Date of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    public bool IsCompletedAllocation { get; set; }

    public List<LeaveAllocationVm> LeaveAllocations { get; set; } = [];
}