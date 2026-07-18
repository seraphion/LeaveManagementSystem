using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services;

public interface ILeaveTypeService
{
    Task<bool> CheckIfLeaveTypeNameExists(string name);
    Task<bool> CheckIfLeaveTypeNameExistsForEdit(string name, int id);
    Task Create(LeaveTypeCreateVM viewData);
    Task Edit(LeaveTypeEditVM viewData);
    Task<T?> Get<T>(int id) where T : class;
    Task<List<LeaveTypeReadOnlyVM>> GetAll();
    Task<bool> LeaveTypeExists(int? id);
    Task Remove(int id);
}
