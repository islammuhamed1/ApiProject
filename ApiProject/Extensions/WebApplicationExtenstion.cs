using Domain.Contracts;

namespace ApiProject.Extensions
{
    public static class WebApplicationExtenstion
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;

        }
    }
}
