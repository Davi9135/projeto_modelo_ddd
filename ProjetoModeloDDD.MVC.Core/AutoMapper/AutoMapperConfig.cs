using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ProjetoModeloDDD.MVC.Core.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            var mappConfig = new MapperConfiguration(map => {
                map.AddProfile(new DomainToViewModelMappingProfile());
                map.AddProfile(new ViewModelToDomainMappingProfile());
            });
            IMapper mapper = mappConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}