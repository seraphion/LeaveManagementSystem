using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes;

public class LeaveTypeReadOnlyVM : LeaveTypeBaseVM
{
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Maximum Allocation of Days")]
    public int Days { get; set; }
}
