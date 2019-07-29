using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.SolarMeasurements.Models;
using Api.Core.SolarMeasurements.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#pragma warning disable 1570

namespace Api.Core.SolarMeasurements.Controllers
{

    //[ApiVersion("1")]
    [Route("api/v1/[controller]")]
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
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger;
            
            _logger.LogDebug($"{nameof(SolarMeasurementsController)} constructed.");
        }

        /// <summary>
        /// Get a set of measurements between two timestamps.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /SolarMeasurements?startTime=2016-01-01&endTime=2016-01-10&granularity=2
        ///
        /// </remarks>
        /// <param name="startTime">Start timestamp</param>
        /// <param name="endTime">End timestamp</param>
        /// <param name="granularity"></param>
        /// <returns></returns>
        /// <response code="200">Returns the requested set of measurements</response>
        /// <response code="400">If no measurements are found</response>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Measurement>>> Get(
            [FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] Granularity granularity)
        {
            _logger.LogDebug($"GetMeasurements: startTime: {startTime}, endTime: {endTime}, granularity: {granularity}");
            var measurements = await _service.GetMeasurements(startTime, endTime, granularity);
            return Ok(measurements);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Measurement>> Get(DateTime timestamp)
        {
            _logger.LogDebug($"GetMeasurement: timestamp: {timestamp}");
            var measurement = await _service.GetMeasurement(timestamp);
            return Ok(measurement);
        }

    }

}