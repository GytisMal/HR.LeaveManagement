using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain;

public class LeaveType : BaseEntity // paveldi BaseEntity, nes isimam is cia properties
// kurie kartojasi, kad isvengti kartojimusi ir bereikalingu kodo eiluciu is to paties rasymo;

{
    public string Name { get; set; } = string.Empty; // it wont be null
    public int DefaultDays { get; set; }
}
