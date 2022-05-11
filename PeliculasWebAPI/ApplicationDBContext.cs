﻿using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;
using PeliculasWebAPI.Entidades.Configuraciones;
using PeliculasWebAPI.Entidades.Seeding;
using PeliculasWebAPI.Entidades.SinLlaves;
using System.Reflection;

namespace PeliculasWebAPI {
    public class ApplicationDBContext : DbContext {
        
        /* Al usar DbContextOptions podemos usar la inyección de dependencias */
        public ApplicationDBContext(DbContextOptions options) : base(options) {

        }

        /* Sirve para configurar una propiedad manual y no por defecto */
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
            configurationBuilder.Properties<DateTime>()
                                .HaveColumnType("date");
        }

        /* API Fluente */
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            /* Implementando la configuracion de la clase Genero */
            /* Aunque este es una forma, pero si tenemos varias configuraciones, sería
             * muchas lineas de código */
            // modelBuilder.ApplyConfiguration(new GeneroConfig());

            /* La forma correcta es por Assembly, el cual scanea en el proyecto
             * todas las configuraciones que heredan de IEntityTypeConfiguration */
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedingModuloConsulta.Seed(modelBuilder);

            /* modelBuilder.Entity<Log>()
                        .Property(l => l.Id)
                        .ValueGeneratedNever(); */

            /* Ignorando una clase */
            // modelBuilder.Ignore<Direccion>();

            modelBuilder.Entity<CineSinUbicacion>()
                        .HasNoKey() /* Hace que la entidad no tenga llave primaria */
                        .ToSqlQuery("SELECT Id, Nombre FROM Cines")
                        .ToView(null); /* Evita que se agruege la tabla con el esquema a la BD */

            modelBuilder.Entity<PeliculaConteos>()
                        .HasNoKey()
                        .ToView("PeliculasConteos");
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<CineOferta> CinesOfertas { get; set; }
        public DbSet<SalaCine> SalasCines { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<CineSinUbicacion> CineSinUbicacion { get; set; }
        public DbSet<PeliculaConteos> PeliculasConteos { get; set; }
    }
}
