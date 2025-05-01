using Services.Abstractions;
using Services.MappingProfiles;
using Services;
using Shared.IdentityDtos;

namespace ApiProject.Extensions
{
    public static class CoreServicesExtenstion
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection("JwtOptions"));
            services.AddAutoMapper(typeof(Services.ServiceManager).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
