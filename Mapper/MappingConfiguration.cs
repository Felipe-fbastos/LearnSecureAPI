using Mapster;
using System.Runtime.CompilerServices;

namespace LearnSecureAPI.Mapper
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMapps(this IServiceCollection services)
        {
            services.AddMapster();

            return services;
        }
    }
}
