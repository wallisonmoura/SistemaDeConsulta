using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.Data.Map
{
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(x => x.CPF)
                .IsUnique();

            builder.Property(x => x.DataNascimento)
                .IsRequired();

            builder.Property(x => x.Sexo)
                .IsRequired();

            builder.Property(x => x.Telefone)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();
        }
    }
}
