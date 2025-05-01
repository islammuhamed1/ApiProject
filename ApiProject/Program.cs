
using ApiProject.Extensions;
using ApiProject.Factories;
using ApiProject.Middlewares;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Services.MappingProfiles;
using Shared.IdentityDtos;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ApiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            builder.Services.AddControllers();
          
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      
          
           
            builder.Services.AddTransient<PictureUrlResolver>();
           


            var app = builder.Build();
            await app.SeedDbAsync();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
       
    }
}
