using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    //IRequestHandler - yra MediatR bibliotekos interface'as - IRequestHandler<TypeRequest, TypeResponse>
    //sis interface nurodo is kur duomenys yra imama(uzklausa) ir nurodo, ka turi grazinti - <List<LeaveTypeDto>
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery,
        List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        //CTOR naudojamas del priklausomybes injection, jog iterpti reikiamas priklausomybes i klase, o ne jas sukurti tiesiogiai.
        //IMapper reikalingas, kad galetu atlikti duomenu atvaizdavima, o ILeaveTypeRepository kad galetu pasiekti duoemnu saltini(metoda)
        public GetLeaveTypesQueryHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<GetLeaveTypesQueryHandler> logger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._logger = logger;
        }

        //kai pateikiama uzklausas i duombaze, kuri apima ivesties / isvesties operacijas, geriausiai atlikti asichroniskai, kad viskas vyktu pagal savo laika
        //Task<List<LeaveTypeDto>> - metodas grazinta TASK, kuris galiau pateiks sarasa <LeaveTypeDto>, kai ji bus baigta
        //Handle yra IRequestHandler sasaja. Jis naudojamas suhandlinti tam tikra uzklausa (siuo atveju GetLeaveTypesQuery), taip pat tai yra metodo pavadinimas.
        //CancellationToken - jis sugeneruojamas automatiskai is interfac'o, kuris leidzia naudotojui bet kada nutraukti metoda.
        //GetLeaveTypesQuery - uzklausos objektas, nes siame metode mes prasome, jog patektu visus duomenis is duombazes.

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, 
            CancellationToken cancellationToken)
        {
            //Query the database - is ILeaveTypeRepository iskvieciamas metodas. Siuo atveju as noriu gauti visus duomenis is duombazes, todel issikvieciu GetAsync metoda.
            //Turite parašyti šią dalį, nes turite nurodyti, kokius duomenis norite gauti. Labai svarbu aiškiai bendrauti su duomenų šaltiniais, norint paprašyti konkrečių jums reikalingų duomenų.
            var leaveTypes = await _leaveTypeRepository.GetAsync();
            //paėmus duomenis iš duomenų bazės, jie paprastai pateikiami domeno objektų arba duomenų modelių pavidalu. Daugeliu atvejų šiuos duomenų objektus norite paversti DTO (duomenų perdavimo objekto)
            //objektais, kurie yra pritaikyti konkretiems naudojimo atvejams arba siuntimui per tinklą. Turite parašyti šį kodą, nes turite nurodyti, kaip turi vykti transformacija. _mapper čia yra AutoMapper, kuris supaprastina šį atvaizdavimo procesą.
            //convert data objects to DTO objects - konvertuoja gauta rezultata is leaveTypes ir perduoda ji i <list<LeaveTypeDto>
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            //return list of DTO object - grazinam gauta rezultata;
            //Tai turite padaryti, kad gautumėte norimą operacijos rezultatą.
            _logger.LogInformation("Leave types were retrieved successfully");
            return data;
        }
        //apibendrinant, sis metodas yra skirtas gauti duomenis is duombazes.
    }
}
