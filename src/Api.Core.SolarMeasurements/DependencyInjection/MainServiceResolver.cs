using System;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.SolarMeasurements.DependencyInjection
{
    public class MainServiceResolver : IServiceProvider
    {
        private static WindsorContainer _container;

        public MainServiceResolver(IServiceCollection services) {
            _container = new WindsorContainer();
            RegisterComponents(_container, services);
        }

        private void RegisterComponents(WindsorContainer container, IServiceCollection services)
        {
            
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}