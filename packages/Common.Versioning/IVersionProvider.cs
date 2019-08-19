using System;
using System.Collections.Generic;

namespace Common.Versioning
{
    public interface IVersionProvider
    {
        VersionInfo VersionInfo { get; }
    }
}