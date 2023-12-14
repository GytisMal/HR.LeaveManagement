using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersintenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. AddDbContext: This method registers a database context with the DI container.
        //    It specifies that the 'HrDatabaseContext' should be used for database operations.
        //    The 'options' lambda is used to configure the database context, specifying the database provider and connection string.
        services.AddDbContext<HrDatabaseContext>(options =>
            // 2. UseSqlServer: This method configures the database context to use SQL Server as the database provider.
            //    It also specifies the connection string to be used for connecting to the SQL Server database.
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString")));

        //uzregistruoja, jog duomenys galetu vaikscioti tarpusavyje;
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

        // 3. Return 'services': This step is optional but commonly used in ASP.NET Core extension methods.
        //    It allows you to chain additional service registrations together if needed.
        return services;
    }
}

