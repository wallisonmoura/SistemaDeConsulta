using SistemaDeConsulta.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Pacientes
{
    public class EditPacienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
