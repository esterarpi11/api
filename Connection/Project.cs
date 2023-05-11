using FamsGames.Model;
using FamsGames.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FamsGames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton(builder.Services.AddDbContext<PosgreSQLConfig>(options => options.UseSqlServer("WebApiDatabase")));
            builder.Services.AddScoped<FamsGamesRepository>();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowedOrigins",
                    policy =>
                    {
                        policy.WithOrigins("*") // note the port is included 
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MyAllowedOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}