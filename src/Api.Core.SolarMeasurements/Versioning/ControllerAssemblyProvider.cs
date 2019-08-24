using System.Reflection;
using Api.Core.SolarMeasurements.Controllers;
using Common.Versioning;

namespace Api.Core.SolarMeasurements.Versioning
{
    public class ControllerAssemblyProvider : IControllerAssemblyProvider
    {
        public Assembly Assembly => Assembly.GetAssembly(typeof(VersionController));
    }
}