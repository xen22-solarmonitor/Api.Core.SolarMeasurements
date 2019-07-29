using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Models;

namespace Api.Core.SolarMeasurements.Repositories
{
    public interface ISolarMeasurementsRepository
    {
        Task<Measurement> GetMeasurement(DateTime timestamp);
        Task<IEnumerable<Measurement>> GetMeasurements(
            DateTime startTimestamp, DateTime endMeasurement, Granularity granularity);
    }
}