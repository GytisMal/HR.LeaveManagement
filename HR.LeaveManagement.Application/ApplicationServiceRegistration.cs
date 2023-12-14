using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application;

//si klase yra skirta uzregistruoti services (panasiai daroma StartUp klaseje)
//ji yra static, nes ji uzregistruoja papildomus komponentus musu projektui
//grazina IServiceCollection, nes kad sujungtu metodus, jog sujungti tuo paciu ir su kitais esamais metodais "grandine"
//this keyword nurodo, kad IServiceCollection yra isplestinis metodas ir mes jam priskiriam
//services, kad galetumem prideti prie jau esamu visu services dar kitus papildinius.
// services.AddAutoMapper(Assembly.GetExecutingAssembly());
// services.AddMediatR(Assembly.GetExecutingAssembly());
//sios dvi kodo eilutes nurodo, kad prie visu servides pridedam dar du papildinius
//ir juos turim grazinti, nes taip sujungia visus services.
//Automapper reikalingas, kad duomenys galetu saugiau vaikscioti is objekto i objekta, taip uztikrinamas sklandus duomenu vaiksciojimas
//MediatR yra kaip bendravimo komponentas per kuri vyksta bendravimas tarp komponentu, jis padeda atsieti programos komponentus ir leidzia tarpusavyje bendradarbiauti.

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
