using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Api.Core.SolarMeasurements.AutoMapper;
using Api.Core.SolarMeasurements.Configuration;
using Api.Core.SolarMeasurements.DependencyInjection;
using Api.Core.SolarMeasurements.Repositories;
using Api.Core.SolarMeasurements.Services;
using Api.Core.SolarMeasurements.Versioning;
using AutoMapper;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Common.Versioning;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api.Core.SolarMeasurements
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // AutoMapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // bind the configuration to our SolarConfig configuration class
            var solarConfig = new SolarConfig();
            Configuration.Bind(solarConfig);

            var apiVersionsRetriever = new SupportedApiVersionsRetriever();
            var apiVersions = apiVersionsRetriever.GetSupportedApiVersions(
                Assembly.GetAssembly(typeof(Startup)));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                foreach (var apiVersion in apiVersions)
                {
                    c.SwaggerDoc($"v{apiVersion}", new OpenApiInfo
                    {
                        Title = GetTitle(),
                            Version = GetVersion(),
                            Description = "A simple API for retrieving measurements from Solar sites",
                            License = new OpenApiLicense
                            {
                                Name = "Licensed under MIT Licence",
                                    Url = new Uri("http://opensource.org/licenses/MIT"),
                            }
                    });
                }

                // TODO: these are deprecated, now depends on serialiser config - check that we get enums as strings
                //c.DescribeAllEnumsAsStrings();
                //c.DescribeStringEnumsInCamelCase();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // DI
            services.AddSingleton(solarConfig);
            services.AddScoped<ISolarMeasurementsService, SolarMeasurementsService>();
            services.AddScoped<ISolarMeasurementsRepository, InfluxDbRepository>();
            services.AddScoped<IInfluxDbClient>((_) =>
                new InfluxDbClient("http://yourinfluxdb.com:8086/",
                    "ciprian", "", InfluxDbVersion.Latest));
            services.AddSingleton<IVersionProvider, GitVersionProvider>();
            services.AddSingleton<IStartupAssemblyProvider, StartupAssemblyProvider>();
            services.AddSingleton<IControllerAssemblyProvider, ControllerAssemblyProvider>();
            services.AddSingleton<ISupportedApiVersionsRetriever, SupportedApiVersionsRetriever>();
            //services.AddSingleton <

            // Add custom provider
            //var container = new MainContainerFactory(services).CreateServiceProvider();
            //return container;

            //            return WindsorRegistrationHelper.CreateServiceProvider(
            //                new WindsorContainer(), services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            //app.UseExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //            app.UseAuthentication();
            //            app.UseAuthorization();
            //            app.UseCors();

            app.UseCors("default");

            // this needs to be after UseAuthentication()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", GetApiName());
            });
        }

        private static string GetApiName()
        {
            return $"{GetTitle()} v{GetVersion()}";
        }

        private static string GetTitle()
        {
            return "SolarMeasurements Core API";
        }

        private static string GetVersion()
        {
            return "1.0";
        }
    }
}