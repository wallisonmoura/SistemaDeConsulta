using Microsoft.EntityFrameworkCore;
using SistemaDeConsulta.Data.EntityConfig;
using SistemaDeConsulta.Data.Map;
using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Paciente> Pacientes => Set<Paciente>();

        public DbSet<TipoExame> TipoExames => Set<TipoExame>();

        public DbSet<Exame> Exames => Set<Exame>();

        public DbSet<Consulta> Consultas => Set<Consulta>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DataBase") + ";TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteConfig());
            modelBuilder.ApplyConfiguration(new TipoExameConfig());
            modelBuilder.ApplyConfiguration(new ExameConfig());
            modelBuilder.ApplyConfiguration(new ConsultaConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
