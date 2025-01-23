namespace LeaveWise.Web.Services.Users;

public class UsersService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    : IUsersService
{
    public async Task<ApplicationUser> GetLoggedInUserAsync() =>
        (await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User))!;

    public async Task<ApplicationUser> GetUserByIdAsync(string userId) => (await userManager.FindByIdAsync(userId))!;

    public async Task<List<ApplicationUser>> GetEmployeesAsync()
    {
        var employees = await userManager.GetUsersInRoleAsync(Roles.Employee);
        return employees.ToList();
    }
}