namespace Api.Core.SolarMeasurements.Models
{
    public class VersionDto
    {
        public string ApiVersion { get; set; }
        public string AssemblyVersion { get; set; }
        public string FullVersion { get; set; }
        public string CommitSha { get; set; }
        public string CommitBranch { get; set; }
        public string BuildTime { get; set; }
        public string BuildConfiguration { get; set; }
    }
}