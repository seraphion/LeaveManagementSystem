using AutoMapper;

using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypeService(ApplicationDbContext context, IMapper mapper) : ILeaveTypeService
{
    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        List<LeaveType> data = await context.LeaveTypes.ToListAsync();
        var viewData = mapper.Map<List<LeaveTypeReadOnlyVM>>(data);

        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }
        var viewData = mapper.Map<T>(data);

        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = await context.LeaveTypes.FindAsync(id);
        if (data != null)
        {
            context.LeaveTypes.Remove(data);
        }

        await context.SaveChangesAsync();
    }

    public async Task Edit(LeaveTypeEditVM viewData)
    {
        var leavetype = mapper.Map<LeaveType>(viewData);
        context.Update(leavetype);
        await context.SaveChangesAsync();
    }

    public async Task Create(LeaveTypeCreateVM viewData)
    {
        var leavetype = mapper.Map<LeaveType>(viewData);
        context.Add(leavetype);
        await context.SaveChangesAsync();
    }

    public async Task<bool> LeaveTypeExists(int? id)
    {
        return await context.LeaveTypes.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        return await context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(string name, int id)
    {
        return await context.LeaveTypes.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()) && x.Id != id);
    }
}
