using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    //si klase LeaveTypeRepository paveldi viska, kas yra GenericRepository klases viduje ir kiekvienam metodui, kur buvo GenericRepository viduje parasyta <T> ar (T entity) 
    // bus pritaikoma entity klase <LeaveType>, todel visi metodai automatiskai prisitaikys visur, kur reikia EntityType ir pakeis i <LeaveType>
    //ILeaveTypeRepository interface paveldi is Application Layer, ir viskas kas siame interface yra ir + ka pats ILeaveTypeRepository interface paveldi;
    //O, siuo atveju is ILeaveTypeRepository paveldi metoda, kurio pavadinimas yra IsLeaveTypeUnique, kuris duombazes lenteleje LeaveTypes tikrina, ar Name yra unikalus;
    //CTOR reikalingas del Dipendency Injection, jog kaskart, kai bus si klase iskviesta, automatiskai integruos ir duombazee HrDatabaseContext kartu su : base (context), kur reikes perduoti context
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {

        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => q.Name == name) == false;
        }
    }
}
