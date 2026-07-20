using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Controllers;

[Authorize(Roles = Roles.Administrator)]
public class LeaveTypesController(ILeaveTypeService leaveTypeService) : Controller
{
    private const string _nameExistsValidationMessage = "This leave type name already exists in the database.";

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()
    {
        List<LeaveTypeReadOnlyVM> viewData = await leaveTypeService.GetAll();

        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

        return View(leavetype);
    }

    // GET: LEAVETYPES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LEAVETYPES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,NumberOfDays")] LeaveTypeCreateVM createVm)
    {
        if (await leaveTypeService.CheckIfLeaveTypeNameExists(createVm.Name))
        {
            ModelState.AddModelError(nameof(createVm.Name), _nameExistsValidationMessage);
        }

        if (!ModelState.IsValid)
        {
            return View(createVm);
        }

        await leaveTypeService.Create(createVm);
        return RedirectToAction(nameof(Index));
    }

    // GET: LEAVETYPES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await leaveTypeService.Get<LeaveTypeEditVM>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

        return View(leavetype);
    }

    // POST: LEAVETYPES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,NumberOfDays")] LeaveTypeEditVM editVm)
    {
        if (id != editVm.Id)
        {
            return NotFound();
        }

        if (await leaveTypeService.CheckIfLeaveTypeNameExistsForEdit(editVm.Name, editVm.Id))
        {
            ModelState.AddModelError(nameof(editVm.Name), _nameExistsValidationMessage);
        }

        if (!ModelState.IsValid)
        {
            return View(editVm);
        }

        try
        {
            await leaveTypeService.Edit(editVm);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await leaveTypeService.LeaveTypeExists(editVm.Id))
            {
                return NotFound();
            }

            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: LEAVETYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

        return View(leavetype);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await leaveTypeService.Remove(id.Value);
        return RedirectToAction(nameof(Index));
    }
}
