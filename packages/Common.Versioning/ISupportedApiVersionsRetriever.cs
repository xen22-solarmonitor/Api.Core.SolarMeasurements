using System.Collections.Generic;
using System.Reflection;

namespace Common.Versioning
{
    public interface ISupportedApiVersionsRetriever
    {
        IEnumerable<string> GetSupportedApiVersions(Assembly controllerAssembly);
    }
}