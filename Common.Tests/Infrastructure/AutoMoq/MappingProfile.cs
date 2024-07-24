using AutoMapper;
using Common.Domain.Entities;
using Common.WebApi.Application.Models.Client;
using System.Reflection;

namespace Common.Tests.Infrastructure.AutoMoq
{
    internal class MappingProfile : Profile
    {
        private Assembly assembly;

        public MappingProfile(Assembly assembly)
        {
            this.assembly = assembly;
            CreateMap<Client, ClientResponseDto>();
        }
    }
}
