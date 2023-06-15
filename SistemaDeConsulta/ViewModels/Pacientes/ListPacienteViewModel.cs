using SistemaDeConsulta.Models.Enums;

namespace SistemaDeConsulta.ViewModels.Pacientes
{
    public class ListTipoExamesViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
