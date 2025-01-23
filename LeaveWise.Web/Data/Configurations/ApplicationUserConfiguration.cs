using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveWise.Web.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "1b36479a-64e0-48aa-983c-73f4fbd78a08",
                Email = "admin@leavewise.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMIN@LEAVEWISE.COM",
                NormalizedUserName = "ADMIN@LEAVEWISE.COM",
                UserName = "admin@leavewise.com",
                PasswordHash = hasher.HashPassword(null!, "P@ssw0rd1"),
                FirstName = "Admin",
                LastName = "Default",
                DateOfBirth = new DateOnly(1999, 1, 1)
            }
        );
    }
}