using System;

namespace Api.Core.SolarMeasurements.Models
{
    public class CurrentVoltageSensorMeasurement : SensorMeasurementBase
    {
        public float VoltageV { get; set; }
        public float CurrentA { get; set; }
    }
}