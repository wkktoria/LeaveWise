using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveWise.Web.Data.Configurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c",
                UserId = "1b36479a-64e0-48aa-983c-73f4fbd78a08"
            }
        );
    }
}