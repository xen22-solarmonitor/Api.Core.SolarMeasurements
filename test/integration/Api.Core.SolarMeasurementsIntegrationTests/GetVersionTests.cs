using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements;
using Api.Core.SolarMeasurementsIntegrationTests.Framework;
using Api.Core.SolarMeasurementsProxy;
using FluentAssertions;
using Xunit;

namespace Api.Core.SolarMeasurementsIntegrationTests
{
    public class GetVersionTests
    {
        private readonly ISolarMeasurementsCoreAPI _client;

        public GetVersionTests()
        {
            CustomWebApplicationFactory<Startup> factory = new CustomWebApplicationFactory<Startup>();
            _client = new SolarMeasurementsCoreAPI(factory.CreateClient(), true);
        }

        private string GetServiceAssemblyLocation()
        {
            var executingAssembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(executingAssembly);
            return Path.Combine(path, "Api.Core.SolarMeasurements.dll");
        }

        [Fact]
        public async Task GetVersion()
        {
            // arrange

            // act
            var version = await _client.Transport.VersionAsync();

            // assert
            version.ApiVersions.Should().BeEquivalentTo(new string[] { "1.0" });

            // use FileVersionInfo to get most properties from the dll
            var versionInfo = FileVersionInfo.GetVersionInfo(GetServiceAssemblyLocation());
            var versionInfoComments = versionInfo.Comments;

            Assert.Equal(versionInfoComments.GetParameter("SemanticVersion"), version.FullVersion);
            Assert.Equal(versionInfoComments.GetParameter("BuildConfig"), version.BuildConfiguration);
            Assert.Equal(versionInfoComments.GetParameter("Branch"), version.CommitBranch);
            Assert.Equal(versionInfoComments.GetParameter("Commit"), version.CommitSha);
            Assert.Equal(versionInfoComments.GetParameter("BuildTime"), version.BuildTime);

            // Assembly version is retrieved from the assembly because FileVersionInfo class
            // does not expose it
            var assembly = Assembly.GetAssembly(typeof(Api.Core.SolarMeasurements.Controllers.VersionController));
            Assert.Equal(assembly.GetName().Version.ToString(), version.AssemblyVersion);

        }
    }

    internal static class VersionInfoCommentsExtensions
    {
        public static string GetParameter(this string versionInfoComments, string parameterName)
        {
            return versionInfoComments.Split(';')
                .Select(entry => entry.Split('='))
                .Where(vals => vals[0].Trim() == parameterName)
                .Select(vals => vals[1])
                .SingleOrDefault();
        }

    }
}