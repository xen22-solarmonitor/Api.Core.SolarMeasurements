using System;

namespace Api.Core.SolarMeasurements.Models
{
    public class ShuntMeasurement
    {
        public DateTime Timestamp { get; set; }
        
        public string Name { get; set; }
        public string Location { get; set; }
        public string SensorType { get; set; }
        
        public float VoltageV { get; set; }
        public float CurrentA { get; set; }
    }
}