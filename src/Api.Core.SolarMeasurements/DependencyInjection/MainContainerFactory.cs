using System;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.SolarMeasurements.DependencyInjection
{
    public class MainContainerFactory
    {
        private readonly IServiceCollection _services;

        public MainContainerFactory(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceProvider CreateServiceProvider()
        {
            return new MainServiceResolver(_services);
        }
    }
}