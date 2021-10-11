using eVendingMachine.Data.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace eVendingMachine.Web.Api.Tests.Integration
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IConfiguration Configuration { get; private set; }
        protected IServiceProvider _serviceProvider;
        protected ISeedDataClass dbSeeder;
        VendingMachineDbContext _db;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json")
                    .Build();

                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<ISeedDataClass, SeedDataClass>();
                _serviceProvider = services.BuildServiceProvider();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    _db = scopedServices.GetRequiredService<VendingMachineDbContext>();
                    dbSeeder = scopedServices.GetRequiredService<ISeedDataClass>();

                    _db.Database.EnsureDeleted();
                    _db.Database.EnsureCreated();
                    dbSeeder.Init();
                }
            });
        }

    }
}
