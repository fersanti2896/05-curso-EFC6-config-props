using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class GeneroConfig : IEntityTypeConfiguration<Genero> {
        /* Colocamos las configuraciones que está en el API Fluente */
        public void Configure(EntityTypeBuilder<Genero> builder) {
            /* Renombrando la tabla Generos por TGeneros */
            //builder.ToTable(name: "TGeneros", schema: "Peliculas");

            /* Hacemos el atributo identificador de la clase Genero llave primaria */
            builder.HasKey(prop => prop.Identificador);

            /* Hace que cuando se hace una peticion get, se omiten los datos que 
             * tiene como status borrado */
            builder.HasQueryFilter(gen => !gen.EstaBorrado);

            /* Damos propiedades al campo Nombre de la clase Genero */
            builder.Property(prop => prop.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

        }
    }
}
