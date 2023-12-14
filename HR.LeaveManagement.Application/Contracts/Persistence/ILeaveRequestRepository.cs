using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    //kai norima gauti tik viena tam tikra israsa
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    //kai norima gauti visus israsus
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    //Kai norima gauti visus vieno darbuotojo israsus
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
}

