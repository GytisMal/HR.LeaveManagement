using HR.LeaveManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest : BaseEntity // paveldi BaseEntity, nes isimam is cia properties
// kurie kartojasi, kad isvengti kartojimusi ir bereikalingu kodo eiluciu is to paties rasymo;
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = string.Empty;

}
