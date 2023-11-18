using BabelAPI.Data;
using BabelAPI.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BabelAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<DUsuario>();
            builder.Services.AddScoped<DRol>();
            builder.Services.AddScoped<DLibro>();
            builder.Services.AddScoped<DCategoria>();

            builder.Services.AddControllers();

            // Configuración del contexto de la base de datos
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("masterconnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
