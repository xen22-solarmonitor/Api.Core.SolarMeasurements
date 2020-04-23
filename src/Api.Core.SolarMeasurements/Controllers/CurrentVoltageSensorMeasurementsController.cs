using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Core.SolarMeasurements.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Core.SolarMeasurements.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class CurrentVoltageSensorMeasurementsController
    {
        private readonly ILogger<CurrentVoltageSensorMeasurementsController> _logger;

        public CurrentVoltageSensorMeasurementsController(ILogger<CurrentVoltageSensorMeasurementsController> logger)
        {
            _logger = logger;
            _logger.LogDebug($"{nameof(CurrentVoltageSensorMeasurementsController)} constructed.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<CurrentVoltageSensorMeasurement>> GetMeasurements(
            [FromQuery, Required] DateTime start, [FromQuery, Required] DateTime end, [FromQuery, Required] Granularity granularity, [FromQuery, Required] ReducerFunction reducer)
        {
            return null;
        }
    }
}