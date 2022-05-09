using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PeliculasWebAPI.Entidades.Configuraciones {
    public class SalaCineConfig : IEntityTypeConfiguration<SalaCine> {
        public void Configure(EntityTypeBuilder<SalaCine> builder) {
            /* Propiedades de la Tabla SalaCine */
            builder.Property(prop => prop.Precio)
                   .HasPrecision(9, scale: 3);

            builder.Property(prop => prop.TipoSalaCine)
                   .HasDefaultValue(TipoSalaCine.DosD);
        }
    }
}
