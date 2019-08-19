using Api.Core.SolarMeasurements.Models;
using AutoMapper;
using Common.Versioning;

namespace Api.Core.SolarMeasurements.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VersionInfo, VersionDto>();
        }
    }
}