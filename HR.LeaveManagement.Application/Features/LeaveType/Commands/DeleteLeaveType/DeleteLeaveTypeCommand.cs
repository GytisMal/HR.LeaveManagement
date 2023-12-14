using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

//<Unit> - panasi esme yra kaip 'void', taciau jis naudojas, kad pateikti, jod metodas ivyko sekmingai ir jokiu pasaliniu trikdziu neatsirado.
//<Unit> - yra MediatR bibliotekos patternas.
//klase turi tik viena property, nes jam reikia tik surasti ir identifikuoti is duombazes LeaveType pagal Id ir istrinti.
public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
