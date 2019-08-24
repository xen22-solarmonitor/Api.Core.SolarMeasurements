using System.Reflection;
using Common.Versioning;

namespace Api.Core.SolarMeasurements.Versioning
{
    public class StartupAssemblyProvider : IStartupAssemblyProvider
    {
        public Assembly Assembly => Assembly.GetAssembly(typeof(Startup));
    }
}