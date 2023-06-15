using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.Data.EntityConfig
{
    public class ExameConfig : IEntityTypeConfiguration<Exame>
    {
        public void Configure(EntityTypeBuilder<Exame> builder)
        {
            builder.ToTable("Exames");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Observacoes)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(x => x.TipoExame)
                .WithMany()
                .HasForeignKey(x => x.TipoExameId);
        }
    }
}
