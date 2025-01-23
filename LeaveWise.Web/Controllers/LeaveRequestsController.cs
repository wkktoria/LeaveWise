using LeaveWise.Web.Models.LeaveRequests;
using LeaveWise.Web.Services.LeaveRequests;
using LeaveWise.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveWise.Web.Controllers;

[Authorize]
public class LeaveRequestsController(ILeaveTypesService leaveTypesService, ILeaveRequestsService leaveRequestsService)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await leaveRequestsService.GetEmployeeLeaveRequestsAsync();
        return View(model);
    }
    
    public async Task<IActionResult> Create(int? leaveTypeId)
    {
        var leaveTypes = await leaveTypesService.GetAllAsync();
        var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
        var model = new LeaveRequestCreateVM
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            LeaveTypes = leaveTypesList
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateVM model)
    {
        if (await leaveRequestsService.RequestDatesExceedAllocationAsync(model))
        {
            ModelState.AddModelError(string.Empty, "You have exceeded the allocation time.");
            ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is invalid.");
        }

        if (ModelState.IsValid)
        {
            await leaveRequestsService.CreateLeaveRequestAsync(model);
            return RedirectToAction(nameof(Index));
        }

        var leaveTypes = await leaveTypesService.GetAllAsync();
        model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        await leaveRequestsService.CancelLeaveRequestAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "AdminSupervisorOnly")]
    public async Task<IActionResult> ListRequests()
    {
        var model = await leaveRequestsService.GetAllLeaveRequestsAsync();
        return View(model);
    }

    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Review(int id)
    {
        var model = await leaveRequestsService.GetLeaveRequestForReviewAsync(id);
        return View(model);
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id, bool approved)
    {
        await leaveRequestsService.ReviewLeaveRequestAsync(id, approved);
        return RedirectToAction(nameof(ListRequests));
    }
}