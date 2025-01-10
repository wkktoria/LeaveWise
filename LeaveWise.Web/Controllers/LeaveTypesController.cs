using Microsoft.EntityFrameworkCore;
using LeaveWise.Web.Models.LeaveTypes;
using LeaveWise.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;

namespace LeaveWise.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class LeaveTypesController(ILeaveTypesService leaveTypesService) : Controller
    {
        private const string NameExistsValidationMessage = "This leave type already exists in the database.";

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            return View(await leaveTypesService.GetAllAsync());
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            if (await leaveTypesService.LeaveTypeNameExistsAsync(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(leaveTypeCreate);
            }

            await leaveTypesService.CreateAsync(leaveTypeCreate);
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await leaveTypesService.GetAsync<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await leaveTypesService.LeaveTypeNameExistsForEditAsync(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(leaveTypeEdit);
            }

            try
            {
                await leaveTypesService.EditAsync(leaveTypeEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await leaveTypesService.LeaveTypeExistsAsync(leaveTypeEdit.Id))
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await leaveTypesService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}