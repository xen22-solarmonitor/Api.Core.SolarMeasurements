using System;

namespace Api.Core.SolarMeasurements.Models
{
    public class SensorMeasurementBase
    {
        public DateTime Timestamp { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string SensorType { get; set; }
    }
}