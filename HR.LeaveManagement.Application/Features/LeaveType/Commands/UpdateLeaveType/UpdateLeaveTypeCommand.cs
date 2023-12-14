using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
//kai naudojame : IRequest<> turime visada kazka grazinti, todel dabar nurodomas yra 
//<Unit>, kuris leidzia grazinti void. 
//klase turi du prop's, kuriuos naudotojas gales Update daryti.
public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}

