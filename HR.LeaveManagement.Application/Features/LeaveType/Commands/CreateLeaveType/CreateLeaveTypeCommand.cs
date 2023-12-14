using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
//Command klases yra skirtos jog sukurti nauja entity objekta. Kaip siuo atveju, norima sukurti nauja LeaveType, kuris tures du pagrindinius parametrus, kuriuos bus privaloma
//nurodyti, t.y. Name ir DefaultDays, jog sukurti nauja entity objekta. O grazinsime tik : IRequest<int>, nes taip nebus apkraunamas serveris, o jei nores pasiziureti duombazeje,
//gales pagal ID issiimti is duombazes. Paveldi IRequest. IRequest - yra MediatR bibliotekos sistema, kuri nurodo, kad tures buti gaunama is CreateLeaveTypeCommand klases
//tad, siuo atveju, :IRequest<int> - nurodo, kad bus privaloma grazinti <int>, o musu int taps ID sukurto naujo LeaveType entity objekto.;

public class CreateLeaveTypeCommand : IRequest<int> // i want to get only the id
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
