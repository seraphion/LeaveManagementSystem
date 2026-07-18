using AutoMapper;

using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Controllers;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private const string _nameExistsValidationMessage = "This leave type name already exists in the database.";

    public LeaveTypesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()
    {
        List<LeaveType> data = await _context.LeaveTypes.ToListAsync();
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);

        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leavetype);

        return View(viewData);
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
        if (await CheckIfLeaveTypeNameExists(createVm.Name))
        {
            ModelState.AddModelError(nameof(createVm.Name), _nameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            var leavetype = _mapper.Map<LeaveType>(createVm);
            _context.Add(leavetype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(createVm);
    }

    // GET: LEAVETYPES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var editVm = _mapper.Map<LeaveTypeEditVM>(leavetype);
        return View(editVm);
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

        if (await CheckIfLeaveTypeNameExistsForEdit(editVm.Name, editVm.Id))
        {
            ModelState.AddModelError(nameof(editVm.Name), _nameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                var leavetype = _mapper.Map<LeaveType>(editVm);
                _context.Update(leavetype);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(editVm.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(editVm);
    }

    // GET: LEAVETYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var deleteVm = _mapper.Map<LeaveTypeReadOnlyVM>(leavetype);
        return View(deleteVm);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype != null)
        {
            _context.LeaveTypes.Remove(leavetype);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LeaveTypeExists(int? id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    private async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    private async Task<bool> CheckIfLeaveTypeNameExistsForEdit(string name, int id)
    {
        return await _context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) && x.Id != id);
    }
}
