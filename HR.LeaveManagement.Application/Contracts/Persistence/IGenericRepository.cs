using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
//Nurodant, kad where T : BaseEntity - nurodoma, kad visi entity types turi tureti panasia struktura, kuris yra nustatytas BaseEntity;
//Todel galiu panaudoti ir kitus entity, kurie paveldi BaseEntity klase;
//Tai yra bendras interface, kuris tures tipe <T>, where - nurodo, kad T yra : klase,
//o klase yra musu sukurti Entity
//Task nurodo, kad metodai bus async'ai <T> tai nurodo bendra tipa (entities) ir metodo pavadimas 
//turi buti su Async, kad zinotumem, jog tai yra Async. 
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

//tada sukuriami atskiri Interface's kiekvienam is entities, kurie gaus metodus
//is bendro sukurto interface (virsuje esantis interface);
//pvz.: kaip turi atrodyti

//public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
//{

//}
//Nurodom, kad tai LeaveType enticio interface - ILeaveTypeRepository
//kuris paveldi IGenericRepository, nes taip paveldes visus esancius metodus is bendro interface (virsuje esantis interface)
//<T> - kur buvo nurodomas bendrame interface entitio tipas, jog tures tam tikra entity klase, dabar nurodoma, kuris entity - <LeaveType>
// ir viska ta pati padarom su kitais turimais entity