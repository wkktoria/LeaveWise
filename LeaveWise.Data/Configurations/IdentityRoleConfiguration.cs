using LeaveWise.Common.Static;

namespace LeaveWise.Data.Configurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "d80ab5af-7746-4240-bc95-aecbd758d97a",
                Name = Roles.Employee,
                NormalizedName = Roles.Employee.ToUpper()
            },
            new IdentityRole
            {
                Id = "6cfb5108-b1b4-42d1-b19f-0bbcefa89133",
                Name = Roles.Supervisor,
                NormalizedName = Roles.Supervisor.ToUpper()
            },
            new IdentityRole
            {
                Id = "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c",
                Name = Roles.Administrator,
                NormalizedName = Roles.Administrator.ToUpper()
            }
        );
    }
}