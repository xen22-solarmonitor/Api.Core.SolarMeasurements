using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Configuration;
using InfluxData.Net.InfluxDb;
using Api.Core.SolarMeasurements.Models;

namespace Api.Core.SolarMeasurements.Repositories
{

    public class InfluxDbRepository : ISolarMeasurementsRepository
    {
        private readonly IInfluxDbClient _dbClient;
        private readonly SolarConfig _config;

        public InfluxDbRepository(IInfluxDbClient dbClient, SolarConfig config)
        {
            _dbClient = dbClient ?? throw new ArgumentNullException(nameof(dbClient));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private (string queryTemplate, object parameters) BuildQuery(string measurement, DateTime timestamp)
        {
            var queryTemplate = $"SELECT * FROM {measurement} "
                   + "WHERE \"time\" = @Timestamp";

            var parameters = new
            {
                @Timestamp = timestamp
            };

            return (queryTemplate, parameters);
        }

        public async Task<Measurement> GetMeasurement(DateTime timestamp)
        {
            var (queryTemplate, parameters) = BuildQuery("shunt", timestamp);
            var result = await _dbClient.Client.QueryAsync(
                queryTemplate,
                parameters,
                _config.InfluxDb.DbName);

            return null;
            //result.Select(m => m.)
        }

        public Task<IEnumerable<Measurement>> GetMeasurements(DateTime startTimestamp, DateTime endMeasurement, Granularity granularity)
        {
            throw new NotImplementedException();
        }
    }
}