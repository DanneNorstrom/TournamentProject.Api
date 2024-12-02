using TournamentProject.Data.Data;

namespace TournamentProject.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TournamentProjectApiContext>();

                try
                {
                    await SeedData.Init(context);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return app;
        }
    }
}
