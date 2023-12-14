using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    //si klase yra skirta sukurti LeaveType klasei Automapper, kad duomenys galetu keliauti is LeaveType klases i DTO klase ir atvirksciai
    // Paveldi : Profile, o Profile yra Automapper bibliotekos klase. Ja turi paveldeti, kad galetumem sukurti duomenims mapp'us.
    //CTOR yra sukuriamas, kad butu galima apibrezti zemelapio taisyskles, is kur ir i kur keliauja duomenys ir atvirksciai
    //CreateMap - sukuria zemelapi <is kur, i kur>() .ReverseMap() - kad duomenys gali keliauti ir atvirksciai.
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>(); 
        }
    }
}
