using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
//Si klase yra atsakinga uz CreateLeaveTypeCommand ivykdyma. Si klase paveldi : IRequestHandler, kuris yra kiles is bibliotekos MadiatR; Jis padeda ivykdyti uzklausa
// <CreateLeaveTypeCommand> - nurodo, kad tai yra request, i kuri turi kreiptis, o <int> - nurodo, koks rezultatas turi buti po request, todel <int> - yra response.
//IRequestHandler<CreateLeaveTypeCommand, int> interface, which indicates that it can handle requests of type CreateLeaveTypeCommand and returns an integer(the ID of the newly created leave type).
//The CreateLeaveTypeCommandHandler class is responsible for handling the CreateLeaveTypeCommand. It processes the command, which involves validation, conversion, and database interaction.
public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    //konstruktorius su private readonly IMapper - duomenu vaizdavimas ir vaiksciojimas tarp klasiu ir duombazes. Ir interface ILeaveTypeRepository - issikviecia metoda is jo.
    //CTOR reikalingas tam, kad kaskart, kai bus sukuriamas naujas klases objektas, turetu siuos properties.
    //The constructor injects two dependencies, IMapper for data mapping and ILeaveTypeRepository for database interaction. Dependency injection ensures that the necessary components are available for handling the command.
    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    //Sis metodas sugeneruojamas automatiskai is : IRequestHandler<CreateLEaveTypeCommand, int>. Sis metodas atsakingas uz naujo LeaveType entity sukurima.
    //Sis metodas turi grazinti Task<int>, kur veliau int tampa Id klases property - CreateLeaveTypeCommand. 
    //The Handle method is responsible for executing the logic of creating a new leave type based on the CreateLeaveTypeCommand request.
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        //iskvieciamas naujas klases objektas su parametru interface'u ILeaveTypeRepository, kad butu galima pritaikyti sukurtas taisykles
        CreateLeaveTypeCommandValidator validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);

        //perduodami metodo parametrai, kurie bus patikrinami, ar atitinka sukurtas taisykles validator.ValidateAsync
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //jei yra error, tai tada ismeta BadRequestException, su string "Invalid LeaveType" - ir tolimesnniu error - validationResult
        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }

        //Conver to domain entity object. Turime konvertuoti CreateLeaveTypeCommand, kurio pavadinimas - request i LeaveType entity, nes jis yra sukuriamas.
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        //add to database. Iskvieciamas metodas is _leaveTypeRepository interface'o - .CreateAsync, kuris atsakingas uz naujo entity LeaveType objekto sukurima.
        //Metodui yra perduodamas parametras (leaveTypeToCreate), nes sis metodas turi sugeneruoti nauja entity objekta LeaveType.
        //Next, it calls the _leaveTypeRepository.CreateAsync(leaveTypeToCreate) method to add the new leave type to the database
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        //return record id. Grazinam id, kuri esame nurode jau params klaseje - CreateLeaveTypeCommand : IRequest<int>. Grazinam tik Id del to, kad 1. serveris nebus taip apkrautas
        //2. grazinant tik Id, user bet kada gales pasiprasyti duomenu pagal Id.
        //Returning just the ID is a common practice to keep the response minimal and efficient.
        return leaveTypeToCreate.Id;
    }
}
