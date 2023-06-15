using SistemaDeConsulta.Models.Enums;

namespace SistemaDeConsulta.Models.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
