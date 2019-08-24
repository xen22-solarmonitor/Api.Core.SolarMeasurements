using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Common.Versioning
{
    public class SupportedApiVersionsRetriever : ISupportedApiVersionsRetriever
    {
        public IEnumerable<string> GetSupportedApiVersions(Assembly controllerAssembly)
        {
            return controllerAssembly
                .DefinedTypes
                .SelectMany(t => t.GetCustomAttributes<ApiVersionAttribute>())
                .SelectMany(a => a.Versions)
                .Select(v => v.ToString())
                .Distinct();
        }
    }
}