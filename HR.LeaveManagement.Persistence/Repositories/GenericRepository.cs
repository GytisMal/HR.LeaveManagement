using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

//Persistence Layer - yra skirtas bendrauti su duombaze;
namespace HR.LeaveManagement.Persistence.Repositories
{
    //Tai bendra klase, kuri <T> turi tureti entity klase;
    //<T> - priskirtas siai klasej, kad galetu istatyti visur, kur yra parasyta parametruose ir return'e <T> ar (T entity), jis automatiskai istatys reiksmes;
    //paveldi :IGenericRepository<T> kur T : yra klase; Sis interface'as yra bendras interface is Application Layer, jog paveldeti siame interface esancius metodus.
    //Nurodant, kad where T : BaseEntity - nurodoma, kad visi entity types turi tureti panasia struktura, kuris yra nustatytas BaseEntity;
    //Todel galiu panaudoti ir kitus entity, kurie paveldi BaseEntity klase;
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        //procected ir readonly todel, kad nebutu galima sio property koreguoti tik cia, niekur kitur;
        protected readonly HrDatabaseContext _context;

        //CTOR reikalingas del Dipendecty Injection, jog kaskart, kai si klase GenericRepository<T (EntityType)> - bus iskviestas, kartu issikvies ir duombaze;
        public GenericRepository(HrDatabaseContext context)
        {
            this._context = context;
        }

        //sie metodai yra paveldeti is Interface'o IGenericRepository<T (EntityType)> ir kiekvienas metodas yra susiejamas su duombaze, nes butent sis Persistance Layer yra skirtas
        //veikti su duombaze;
        //parametru vietose nurodyti (T entity) - nurodo, kad parametruose tures buti entity Class;
        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        //.AsNoTracking() - metodas naudojamas kai norima TIK perskaityti gautus duomenis, nenorint ju modifikuoti, pakeisti
        // jis padidna efektyvuma ir sumazina atminties naudojima;
        //be .AsNoTracking() metodo, pateikus uzklausa i duombaze, EF core pradeda sekti siuos objektus konteksto pakeitimo stebejimo priemoneje, o tai reikalauja papildomos atminties.
        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        //.AsNoTracking() - metodas naudojamas kai norima TIK perskaityti gautus duomenis, nenorint ju modifikuoti, pakeisti
        // jis padidna efektyvuma ir sumazina atminties naudojima;
        //be .AsNoTracking() metodo, pateikus uzklausa i duombaze, EF core pradeda sekti siuos objektus konteksto pakeitimo stebejimo priemoneje, o tai reikalauja papildomos atminties.
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
