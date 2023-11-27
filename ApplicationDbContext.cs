using BabelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BabelAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Llama a los modelos para realizar la migración
        public DbSet<MUsuario> Usuarios { get; set; }
        public DbSet<MRol> Roles{ get; set; }
        public DbSet<MLibro> Libros{ get; set; }
        public DbSet<MEventoNoticia> EventosNoticias{ get; set; }
        public DbSet<MCategoria> Categorias{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales del modelo si las necesitas.
        }
    }
}
