using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity // paveldi BaseEntity, nes isimam is cia properties
// kurie kartojasi, kad isvengti kartojimusi ir bereikalingu kodo eiluciu is to paties rasymo;
{
    public int NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}
