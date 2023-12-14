using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

//sis interface nurodo deklaruojama metoda, kuris turi grazinti bool variable
//turi viena parameter - string name;
public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string name);
}

