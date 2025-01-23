using System.Reflection;
using Microsoft.EntityFrameworkCore;
using LeaveWise.Web.Services.Email;
using LeaveWise.Web.Services.LeaveAllocations;
using LeaveWise.Web.Services.LeaveRequests;
using LeaveWise.Web.Services.LeaveTypes;
using LeaveWise.Web.Services.Periods;
using LeaveWise.Web.Services.Users;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
builder.Services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
builder.Services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
builder.Services.AddScoped<IPeriodsService, PeriodsService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminSupervisorOnly", policy => { policy.RequireRole(Roles.Administrator, Roles.Supervisor); });

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();