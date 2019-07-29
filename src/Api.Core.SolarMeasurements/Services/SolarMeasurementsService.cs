using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Constants;
using Api.Core.SolarMeasurements.Models;
using Api.Core.SolarMeasurements.Repositories;
using Api.Core.SolarMeasurements.Extensions;

namespace Api.Core.SolarMeasurements.Services
{
    public class SolarMeasurementsService : ISolarMeasurementsService
    {
        private readonly ISolarMeasurementsRepository _repository;
        public SolarMeasurementsService(ISolarMeasurementsRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Measurement> GetMeasurement(DateTime timestamp)
        {
            // parameter validation
            timestamp
                .CheckYearGreaterThanOrThrow(GeneralConstants.InitialYear)
                .CheckDateInThePastOrThrow();

            return await _repository.GetMeasurement(timestamp);
        }

        public async Task<IEnumerable<Measurement>> GetMeasurements(DateTime startTimestamp, DateTime endTimestamp, Granularity granularity)
        {
            // parameter validation
            endTimestamp.CheckDateInThePastOrThrow();
            startTimestamp
                .CheckOlderThanOrThrow(endTimestamp)
                .CheckYearGreaterThanOrThrow(GeneralConstants.InitialYear);

            return await _repository.GetMeasurements(startTimestamp, endTimestamp, granularity);
        }
    }

}