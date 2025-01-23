using LeaveWise.Application.Models.LeaveAllocations;
using LeaveWise.Application.Services.LeaveAllocations;
using LeaveWise.Application.Services.LeaveTypes;

namespace LeaveWise.Web.Controllers;

[Authorize]
public class LeaveAllocationsController(
    ILeaveAllocationsService leaveAllocationsService,
    ILeaveTypesService leaveTypesService) : Controller
{
    [Authorize(Policy = "AdminSupervisorOnly")]
    public async Task<IActionResult> Index()
    {
        var employees = await leaveAllocationsService.GetEmployeesAsync();
        return View(employees);
    }

    [Authorize(Policy = "AdminSupervisorOnly")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(string? id)
    {
        await leaveAllocationsService.AllocateLeaveAsync(id!);
        return RedirectToAction(nameof(Details), new { userId = id });
    }

    public async Task<IActionResult> Details(string? userId)
    {
        var employeeVm = await leaveAllocationsService.GetEmployeeAllocationsAsync(userId);

        return View(employeeVm);
    }

    [Authorize(Policy = "AdminSupervisorOnly")]
    public async Task<IActionResult> EditAllocation(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var allocation = await leaveAllocationsService.GetEmployeeAllocationAsync(id.Value) ?? null;
        if (allocation == null)
        {
            return NotFound();
        }

        return View(allocation);
    }

    [Authorize(Policy = "AdminSupervisorOnly")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAllocation(LeaveAllocationEditVm allocation)
    {
        if (await leaveTypesService.DaysExceedMaximumAsync(allocation.LeaveType.Id, allocation.Days))
        {
            ModelState.AddModelError("Days", "The allocation exceeds the maximum leave type value.");
        }

        if (!ModelState.IsValid)
        {
            var days = allocation.Days;
            
            allocation = await leaveAllocationsService.GetEmployeeAllocationAsync(allocation.Id);
            allocation.Days = days;
            
            return View(allocation);
        }

        await leaveAllocationsService.EditAllocationAsync(allocation);
        return RedirectToAction(nameof(Details), new { userId = allocation.Employee!.Id });
    }
}