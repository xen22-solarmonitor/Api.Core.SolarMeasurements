using System;

namespace Api.Core.SolarMeasurements.Models
{
    public class Measurement
    {
        public DateTime Timestamp { get; set; }
        public int SensorId { get; set; }
        public double Value { get; set; }

    }
}