using AutoMapper;
using System.Reflection;

namespace Common.Tests.Infrastructure.AutoMoq
{
    internal class MappingProfile : Profile
    {
        private Assembly assembly;

        public MappingProfile(Assembly assembly)
        {
            this.assembly = assembly;
            // CreateMap<SourceType, DestinationType>();
        }
    }
}
