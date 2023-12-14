using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
//: IRequestHandler<DeleteLeaveTypeCommand, Unit> - si klase paveldi IRequstHandleri, kuris turi requesti ir jo response yra Unit, kuris praktiskai nieko negrazina, tik nurodo, ar sekmingai ivyko metodas.
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        //IMapper nera reikalingas, nes duomenys nera transformuoji is vienos klases i kita. 
        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //retrieve domain entity object
            //pirma surandamas norimas irasas istrinti is duombazes
            //paduodamas yra is DeleteLeaveTypeCommand klases Id, pagal kuri turi ieskoti
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            //verify that record exists
            //nameof - naudojamas tam, kad pasikeitus LeaveType name, visada parodytu esanti dabartini
            //taip yra isvengiamas Hardcodinimas.
            //request.Id - naudojamas tam, kad parodytumem, koki useris issiunte Id kaip requesta.
            if (leaveTypeToDelete == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //remove from database
            //gautas rezultatas yra pateikiamas istrinimo metodui;
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            //return record id;
            //grazinamas Unit, jog parodyti, kad viskas ivyko sekmingai, be error, panasiai kaip 'void'
            return Unit.Value;
        }
    }
}
