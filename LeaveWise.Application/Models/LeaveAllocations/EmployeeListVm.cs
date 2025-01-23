using System.ComponentModel.DataAnnotations;

namespace LeaveWise.Application.Models.LeaveAllocations;

public class EmployeeListVm
{
    public string Id { get; set; } = string.Empty;

    [Display(Name = "First Name")] public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")] public string LastName { get; set; } = string.Empty;

    [Display(Name = "Email Address")] public string Email { get; set; } = string.Empty;
}