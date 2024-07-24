using AutoMapper;
using Common.Domain.Entities;
using Common.WebApi.Application.Models.Client;
using Common.WebApi.Application.Models.Photographer;
using Common.WebApi.Application.Models.Location;
using Common.WebApi.Application.Models.Session;

namespace Common.WebApi.Application.Mappings
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientRequestDto>().ReverseMap();
            CreateMap<Photographer, PhotographerDto>().ReverseMap();
            CreateMap<Session, SessionDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }

}
