using ApiProject.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Extensions
{
    public static class PresentationServiceExtenstion
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
