using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Models;

namespace Api.Core.SolarMeasurements.Services
{
    public interface ISolarMeasurementsService
    {
        Task<Measurement> GetMeasurement(DateTime timestamp);
        Task<IEnumerable<Measurement>> GetMeasurements(DateTime startTimestamp, DateTime endTimestamp, Granularity granularity);
    }
}