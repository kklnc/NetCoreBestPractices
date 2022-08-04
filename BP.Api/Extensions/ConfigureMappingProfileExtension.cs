using AutoMapper;
using BP.Api.Data.Models;
using BP.Api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BP.Api.Extensions
{
    public static class ConfigureMappingProfileExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(i => i.AddProfile(new AutoMapperMappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<Contact, ContactDTO>()
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.Name + " " + z.LastName))
                .ReverseMap();
        }
    }
}
