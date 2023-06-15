using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.Data.EntityConfig
{
    public class TipoExameConfig : IEntityTypeConfiguration<TipoExame>
    {
        public void Configure(EntityTypeBuilder<TipoExame> builder)
        {
            builder.ToTable("Tipo_Exames");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
