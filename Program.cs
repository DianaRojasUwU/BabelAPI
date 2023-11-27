using BabelAPI.Data;
using BabelAPI.Datos;
using Microsoft.EntityFrameworkCore;

namespace BabelAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Crea un constructor de aplicaciones web
            var builder = WebApplication.CreateBuilder(args);

            // Añade servicios al contenedor de servicios.
            builder.Services.AddScoped<DUsuario>();     // Servicio para manejar operaciones relacionadas con usuarios
            builder.Services.AddScoped<DRol>();         // Servicio para manejar operaciones relacionadas con roles
            builder.Services.AddScoped<DLibro>();       // Servicio para manejar operaciones relacionadas con libros
            builder.Services.AddScoped<DCategoria>();   // Servicio para manejar operaciones relacionadas con categorías
            builder.Services.AddScoped<DEventoNoticia>();// Servicio para manejar operaciones relacionadas con eventos y noticias

            // Añade los controladores dentro de la solución
            builder.Services.AddControllers();

            // Configuración del contexto de la base de datos usando Entity Framework Core
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("masterconnection")));

            // Configuración de CORS (Cross-Origin Resource Sharing)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Configuración de Swagger/OpenAPI para documentar y explorar la API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Construye la aplicación
            var app = builder.Build();

            // Configura el pipeline de solicitud HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();    // Habilita Swagger en entorno de desarrollo
                app.UseSwaggerUI();  // Configura la interfaz de usuario de Swagger
            }

            app.UseHttpsRedirection(); // Redirige automáticamente las solicitudes HTTP a HTTPS

            // Habilita CORS para permitir solicitudes desde cualquier origen
            app.UseCors();

            app.UseAuthorization(); // Habilita la autorización

            app.MapControllers();    // Mapea los controladores para manejar las solicitudes HTTP

            app.Run();
        }
    }
}
