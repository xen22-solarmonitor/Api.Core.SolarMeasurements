using Api.Core.SolarMeasurements.Models;
using AutoMapper;
using Common.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core.SolarMeasurements.Controllers
{
    //[ApiVersionNeutral]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class VersionController : ControllerBase
    {
        private readonly IVersionProvider _versionProvider;
        private readonly IMapper _mapper;

        public VersionController(IVersionProvider versionProvider, IMapper mapper)
        {
            _versionProvider = versionProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns version information about this service
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult<VersionDto> GetVersion()
        {
            return Ok(_mapper.Map<VersionDto>(_versionProvider.VersionInfo));
        }
    }
}