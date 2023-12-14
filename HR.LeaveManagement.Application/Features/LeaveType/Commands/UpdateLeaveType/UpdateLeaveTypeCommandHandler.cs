using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

        //cia gaunasi Dipendecy Injection, nes kaskart, kai si klase bus inicializuojama, automatiskai suveiks ir IMapper ir ILeaveTypeRepository, o jie abu yra
        //naudojami metode. Jei nebutu CTOR, neveiktu ir metodas;
        public UpdateLeaveTypeCommandHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data
            UpdateLeaveTypeCommandValidator validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}",
                    nameof(LeaveType), request.Id);

                throw new BadRequestException("Invalid leave type", validationResult);
            }

            //convert to domain entity object
            //IMapper siuo atveju konvertuoja duomenis, kurie buvo gauti i UpdateLeaveTypeCommand
            //ir juos perkonvertuoja i Domain.LeaveType entity, kad galetu sudeti atnaujintus duomenis i duombazes lentele, 
            //kuri priklause LeaveType entity.
            // request - perduodami duomenys.
            //<Domain.LeaveType> - i kur perduodami duomenys.
            var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //add to datavase
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            //return Unit value
            //Grazina Unit.Value, jog nurodytu, kad metodas ivyko sekmingai ir nera perduodamu reiksmingu rezultato duomenu.
            return Unit.Value;
        }
    }
}
