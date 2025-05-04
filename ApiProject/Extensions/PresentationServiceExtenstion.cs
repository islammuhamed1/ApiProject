using ApiProject.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
            services.ConfigureSwagger();
            return services;
        }
        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Islam API",
                    Version = "v1",
                    Description = "My API Description"
                });
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter Bearer Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

    }
}
