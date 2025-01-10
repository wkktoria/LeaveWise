using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveWise.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "d80ab5af-7746-4240-bc95-aecbd758d97a",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "6cfb5108-b1b4-42d1-b19f-0bbcefa89133",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR"
            },
            new IdentityRole
            {
                Id = "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );

        var hasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>().HasData(
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

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c",
                UserId = "1b36479a-64e0-48aa-983c-73f4fbd78a08"
            }
        );
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public DbSet<Period> Periods { get; set; }
}