using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.Data.EntityConfig
{
    public class ConsultaConfig : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consultas");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Paciente)
                .WithMany()
                .HasForeignKey(x => x.PacientId);

            builder.HasOne(x => x.Exame)
                .WithMany()
                .HasForeignKey(x => x.ExameId);

            builder.Property(x => x.DataHora)
                .IsRequired();

            builder.Property(x => x.NumeroProtocolo)
                .IsRequired(); 
        }
    }
}
