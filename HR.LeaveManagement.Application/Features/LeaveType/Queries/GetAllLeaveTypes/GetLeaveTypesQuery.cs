using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    //public class GetLeaveTypesQuery :IRequest<List<LeaveTypeDto>>
    //{
    //}

    //record klase yra naudojama, kai norima apibrezti nekintama duomenu struktura.
    //record klases naudojamos duomenimis, kurie po susikurimo neturetu keistis, o tik reprezentuoti. 
    //IRequest yra is bibliotektos MadiatR, siame kontekste jis reiskia uzklausos objekta.
    //jis taip pat paveldedamas nurodo, kad gali buti naudojamas kaip uzklausos objektas.
    //<List<LeaveTypeDto> - nurodo IRequest uzklausos grazinimo tipa, kurio tikimasi is sios uzklausos IRequest
    //apibendrinant, tai si klase GetLeaveTypesQuery nurodo, kad turi gauti sarasa is klases LeaveTypeDto.
    //QUERY zodis yra pasitelkiamas, kad suprasti, jog tai yra DUOMENU UZKLAUSA
    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;

}
