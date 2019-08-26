using System.Reflection;

namespace Common.Versioning
{
    public interface IAssemblyProvider
    {
        Assembly Assembly { get; }
    }

    public interface IControllerAssemblyProvider : IAssemblyProvider { }
    public interface IStartupAssemblyProvider : IAssemblyProvider { }
}