using System.Reflection;
using LeaveWise.Application.Services.Email;
using LeaveWise.Application.Services.LeaveAllocations;
using LeaveWise.Application.Services.LeaveRequests;
using LeaveWise.Application.Services.LeaveTypes;
using LeaveWise.Application.Services.Periods;
using LeaveWise.Application.Services.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveWise.Application;

public static class ApplicationServicesRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<ILeaveTypesService, LeaveTypesService>();
        services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
        services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
        services.AddScoped<IPeriodsService, PeriodsService>();
        services.AddScoped<IUsersService, UsersService>();
        
        services.AddTransient<IEmailSender, EmailSender>();
    }
}