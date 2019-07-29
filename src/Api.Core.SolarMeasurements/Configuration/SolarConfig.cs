// ReSharper disable All
namespace Api.Core.SolarMeasurements.Configuration
{
    // sample configuration
    public class SolarConfig
    {
        public InfluxDb InfluxDb { get; set; }
    }

    public class InfluxDb
    {
        public string DbServer { get; set; } = "influxdb";
        public string DbName { get; set; } = "solardb";
        public string Username { get; set; } = "user";
    }
}
