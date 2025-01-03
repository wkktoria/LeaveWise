using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveWise.Web.Data;

public class LeaveType
{
    [Key] public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")] public required string Name { get; set; }

    public int NumberOfDays { get; set; }
}