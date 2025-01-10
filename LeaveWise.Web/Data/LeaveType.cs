using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveWise.Web.Data;

public class LeaveType : BaseEntity
{
    [Column(TypeName = "nvarchar(150)")] public required string Name { get; set; }

    public int NumberOfDays { get; set; }

    public List<LeaveAllocation>? LeaveAllocations { get; set; }
}