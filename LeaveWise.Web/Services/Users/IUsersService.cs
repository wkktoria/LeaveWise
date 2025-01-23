namespace LeaveWise.Web.Services.Users;

public interface IUsersService
{
    Task<ApplicationUser> GetLoggedInUserAsync();

    Task<ApplicationUser> GetUserByIdAsync(string userId);

    Task<List<ApplicationUser>> GetEmployeesAsync();
}