using System;
using Api.Core.SolarMeasurements;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Core.SolarMeasurementsIntegrationTests.Framework
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(collection =>
            {
                //_configureTestServices?.Invoke(collection);
            });

            // builder.ConfigureServices(services =>
            // {
            //     // Create a new service provider.
            //     var serviceProvider = new ServiceCollection()
            //         //.AddEntityFrameworkInMemoryDatabase()
            //         .BuildServiceProvider();

            //     // Add a database context (AppDbContext) using an in-memory database for testing.
            //     // services.AddDbContext<AppDbContext>(options =>
            //     // {
            //     //     options.UseInMemoryDatabase("InMemoryAppDb");
            //     //     options.UseInternalServiceProvider(serviceProvider);
            //     // });

            //     // Build the service provider.
            //     var sp = services.BuildServiceProvider();

            //     // Create a scope to obtain a reference to the database contexts
            //     using(var scope = sp.CreateScope())
            //     {
            //         var scopedServices = scope.ServiceProvider;
            //         //var appDb = scopedServices.GetRequiredService<AppDbContext>();

            //         var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            //         // Ensure the database is created.
            //         //appDb.Database.EnsureCreated();

            //         try
            //         {
            //             // Seed the database with some specific test data.
            //             //SeedData.PopulateTestData(appDb);
            //         }
            //         catch (Exception ex)
            //         {
            //             logger.LogError(ex, "An error occurred seeding the " +
            //                 "database with test messages. Error: {ex.Message}");
            //         }
            //     }
            // });
        }
    }
}