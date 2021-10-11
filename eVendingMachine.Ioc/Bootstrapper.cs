using eVendingMachine.Commands;
using eVendingMachine.Data.EF;
using eVendingMachine.Domain.Service;
using eVendingMachine.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace eVendingMachine.Ioc
{
    public static class Bootstrapper
    {
        public static void AddeVendingMachine(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabase(services, configuration);
            AddMediator(services);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddClassesInterfaces(new[] { typeof(EFRepository<>).Assembly });
            AddDomainServices(services);

        }
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICashChanger, CashChanger>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(VendingMachineDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<VendingMachineDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("eVendingMachineDB"),
                sql => sql.MigrationsAssembly("eVendingMachine.Data.EF.Migrations"))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());
        }
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(AddProductCommand)));
            services.AddMediatR(Assembly.GetAssembly(typeof(GetCashQuery)));
        }


        private static void AddClassesInterfaces(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());
        }
    }
}
