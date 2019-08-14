using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Models;
using Api.Core.SolarMeasurements.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#pragma warning disable 1570

namespace Api.Core.SolarMeasurements.Controllers
{

    // TODO: enable API versioning when Microsoft.AspNetCore.Mvc.Versioning package supports .NET Core 3.0
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    // TODO: revert back to v1 instead of v1.0 when autorest supports it
    [Route("api/v1.0/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SolarMeasurementsController : ControllerBase
    {
        private readonly ILogger<SolarMeasurementsController> _logger;
        private readonly ISolarMeasurementsService _service;

        public SolarMeasurementsController(
            ISolarMeasurementsService service,
            ILogger<SolarMeasurementsController> logger)
        {
            _service = service ??
                throw new ArgumentNullException(nameof(service));
            _logger = logger;

            _logger.LogDebug($"{nameof(SolarMeasurementsController)} constructed.");
        }

        /// <summary>
        /// Get a set of measurements between two timestamps.
        /// test 4 (TODO: remove)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /SolarMeasurements?startTime=2016-01-01T12:00:00&endTime=2016-01-10T12:00:00&granularity=2
        ///
        /// </remarks>
        /// <param name="startTime">Start timestamp</param>
        /// <param name="endTime">End timestamp</param>
        /// <param name="granularity">how granular should the results be</param>
        /// <returns></returns>
        /// <response code="200">Returns the requested set of measurements</response>
        /// <response code="400">If no measurements are found</response>
        /// <response code="404">If no measurements found</response>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Measurement>>> Get(
            [FromQuery][Required] DateTime startTime, [FromQuery][Required] DateTime endTime, [FromQuery][Required] Granularity granularity)
        {
            _logger.LogDebug($"GetMeasurements: startTime: {startTime}, endTime: {endTime}, granularity: {granularity}");
            var measurements = await _service.GetMeasurements(startTime, endTime, granularity);
            return Ok(measurements);
        }

        // GET api/values/5
        [HttpGet("{timestamp}")]
        public async Task<ActionResult<Measurement>> Get(DateTime timestamp)
        {
            _logger.LogDebug($"GetMeasurement: timestamp: {timestamp}");
            var measurement = await _service.GetMeasurement(timestamp);
            return Ok(measurement);
        }

    }

}