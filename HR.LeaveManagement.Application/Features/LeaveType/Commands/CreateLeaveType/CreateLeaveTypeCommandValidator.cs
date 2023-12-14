using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

//FluentValidation - .NET bibliotekos papildinys, kuris padeda biznio logikai priskirti tam tikras taisykles,
//kurios yra aprasytos atskirtai nuo biznio logikos klases.
//klase paveldi FluentValidation bibliotekos AbstractValidator<T> - paveldint sia klase, nurodome, kad sioje klaseje
//taikysim taisykles, o <CreateLeaveTypeCommand> - nurodoma, kad visos taisykles bus taikomos sios klases objektams.
public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    public ILeaveTypeRepository _leaveTypeRepository;

    //privaloma sukurti CTOR, kad pritaikyti sukurtas taisukles klases objektams.
    //siuo atveju pritaikoma Name ir Default days, o Name dar turi papildoma taisykle, jog patikrina
    //ar Name property yra unikalus, kad sistemoje neegzistuoja daugiau nei viena karta.
    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Name)
        .NotEmpty().WithMessage("{PropertyName} is required")
        .NotNull()
        .MaximumLength(70).WithMessage("{PropertyName must be fewer than 70 characters.}");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type already exists");

        this._leaveTypeRepository = leaveTypeRepository;
    }

    //sis metodas yra is interface'o ILeaveTypeRepository, kuris grazina bool (true/false)
    //turi du parametrus CreateLeaveTypeCommand = command ir CancellationToken = cancellationToken
    //jis tikrina ar CreateLeaveTypeCommand klaseje nurodytas atostogu Name yra uniklas = command.Name;
    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
