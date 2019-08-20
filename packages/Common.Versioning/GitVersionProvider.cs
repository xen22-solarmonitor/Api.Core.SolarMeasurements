using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Common.Versioning
{
    /// <summary>
    /// This implementation retrieves version information from the the main assembly and assumes
    /// that the assembly has been versioned using GitVersion at build time following this pattern:
    /// - AssemblyVersion 
    /// - Description contains semicolon delimited parameter=value, where the following parameters are
    ///   expected:
    ///     - semanticVersion
    ///     - commit
    ///     - branch
    ///     - buildTime
    ///     - buildConfig
    /// </summary>
    public class GitVersionProvider : IVersionProvider
    {
        enum AssemblyDescriptionField
        {
            SemanticVersion,
            Commit,
            Branch,
            BuildTime,
            BuildConfig
        }
        private ILogger<GitVersionProvider> _logger;
        private Dictionary<AssemblyDescriptionField, string> _descriptionFields;

        public GitVersionProvider(ILogger<GitVersionProvider> logger)
        {
            _logger = logger;

            _descriptionFields = new Dictionary<AssemblyDescriptionField, string>();
            var entryAssembly = Assembly.GetEntryAssembly();
            _logger.LogDebug($"Retrieving Description field from assembly {entryAssembly.GetName()}");
            var assemblyDescription = entryAssembly
                .GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            if (assemblyDescription == null)
            {
                logger.LogWarning($"Could not get Description field from assembly {entryAssembly.FullName}");
                return;
            }

            var descrFields = assemblyDescription.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var f in descrFields)
            {
                var entry = f.Split('=');
                if (entry.Count() != 2)
                {
                    logger.LogWarning($"Ignoring malformed field '{f}' from assembly Description entry.");
                    continue;
                }
                var result = Enum.TryParse(entry[0].Trim(), out AssemblyDescriptionField key);
                if (!result)
                {
                    logger.LogWarning($"Ignoring unknown key '{entry[0]}' from assembly Description entry.");
                    continue;
                }
                _descriptionFields[key] = entry[1].Trim();
            }
        }

        public VersionInfo VersionInfo => new VersionInfo
        {
            ApiVersion = "1.0",
            AssemblyVersion = Assembly.GetEntryAssembly().GetName().Version.ToString(),
            FullVersion = _descriptionFields.GetValueOrDefault(AssemblyDescriptionField.SemanticVersion),
            CommitSha = _descriptionFields.GetValueOrDefault(AssemblyDescriptionField.Commit),
            CommitBranch = _descriptionFields.GetValueOrDefault(AssemblyDescriptionField.Branch),
            BuildTime = _descriptionFields.GetValueOrDefault(AssemblyDescriptionField.BuildTime),
            BuildConfiguration = _descriptionFields.GetValueOrDefault(AssemblyDescriptionField.BuildConfig),
        };

    }
}