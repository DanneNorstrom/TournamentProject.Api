using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;

//using Services.Contracts;
//using TournamentProject.Services;
using TournamentProject.Core.Repositories;
using TournamentProject.Data.Data;
using TournamentProject.Data.Repositories;
using TournamentProject.Services;
//using TournamentProject.Services;

namespace TournamentProject.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentProjectApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentProjectApiContext") ?? throw new InvalidOperationException("Connection string 'TournamentProjectApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUoW, UoW>();

            builder.Services.AddAutoMapper(typeof(TournamentMappings));

            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddScoped<ITournamentService, TournamentService>();
            builder.Services.AddScoped<IGameService, GameService>();
            //builder.Services.AddLazy<ITournamentService>();
            //builder.Services.AddLazy<IGameService>();

            builder.Services.AddScoped(provider => new Lazy<ITournamentService>(() => provider.GetRequiredService<ITournamentService>()));
            builder.Services.AddScoped(provider => new Lazy<IGameService>(() => provider.GetRequiredService<IGameService>()));


            var app = builder.Build();

            //await app.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
