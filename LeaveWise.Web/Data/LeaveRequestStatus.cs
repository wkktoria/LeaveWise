namespace LeaveWise.Web.Data;

public class LeaveRequestStatus : BaseEntity
{
    [StringLength(50)]
    public required string Name { get; set; }
}