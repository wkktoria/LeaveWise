using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveWise.Web.Models.LeaveRequests;

public class LeaveRequestCreateVM : IValidatableObject
{
    [Required] [DisplayName("Start Date")] public DateOnly StartDate { get; set; }

    [Required] [DisplayName("End Date")] public DateOnly EndDate { get; set; }

    [Required]
    [DisplayName("Desired Leave Type")]
    public int LeaveTypeId { get; set; }

    [StringLength(250)]
    [DisplayName("Additional Information")]
    public string? RequestComments { get; set; }

    public SelectList? LeaveTypes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
        {
            yield return new ValidationResult("The start date must be before the end date.",
                [nameof(StartDate), nameof(EndDate)]);
        }
    }
}