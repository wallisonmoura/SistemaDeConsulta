using SistemaDeConsulta.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Pacientes
{
    public class CreatePacienteViewModel
    {
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Campo obrigatório")]
        public DateTime? DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        public string Telefone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
