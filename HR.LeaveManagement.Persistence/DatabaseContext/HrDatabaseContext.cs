using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    //apibrezta duombaze turi paveldeti is EntityFrameworkCore :DbContext, taip nurodome, kad HrDatabaseContext yra duombaze
    public class HrDatabaseContext : DbContext
    {
        //sukuriamas konstruktorius, kuris kaskart, kai bus iskvieciamas, inicializuotu kitus metodus ir pritaikytu nustatymus.
        //DbContextOptions<HrDatabaseContext> options - nurodo kaip turi veikti duombaze
        // : base (options) - yra CTOR iniciatorius. jis nurodo, kad turi buti iskviestas sy 'options' parametrus.
        public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options) : base (options)
        {
            
        }

        //cia nurodomi entities, is kuriu bus daromos lenteles duombazeje
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations{ get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        //sis metodas yra is DbContext klases. Sis metodas yra skirtas duomenu bazes modeliui (schemai) konfiguruoti pagal
        //programoje apibreztas objektu klases.
        //is esmes sis metodas yra atsakingas uz visas kostiumizacijas (pakeitimus) duombazeje.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        //sis metodas taip pat yra is DbContext klases. Sis metodas yra kaskart iskvieciamas, kai issaugomi duomenis i duombaze,
        //naudojamas foreach, job pereiti per visus entities, kurie buvo pakeisti ar atnaujinti - base.ChangeTracker.Entries<BaseEntity>();
        //.Where - filtruoja, kur buvo atlikti pakeitimai. Tai, ar buvo pridetas naujas entity q.State == EntityState.Added
        // || q.State == EntityState.Modified - ar Entity buvo pakoreguotas.
        //jei taip, tai tada is <BaseEntity> klases properties enty.Entity.DateModified yra pakeiciamas i dabartini laikas = DateTime.Now;
        //O, jei yra pridetas naujas israsas, tai tada DateCreadted yra pakeiciamas taip pat i entry.Entity.DateCreated = DateTime.Now;
        //Task<int> - grazinta int, nes EF Core reprezentuoja kuri eilute is duombazes buvo pakoreguota, atnaujinta ar prideta.
        //priezastis, kodel grazina int - tai del error handling, jog pranestu, kurios eilutes yra blogai. Taip pat pranesa, kurios eilutes buvo paliestos.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
