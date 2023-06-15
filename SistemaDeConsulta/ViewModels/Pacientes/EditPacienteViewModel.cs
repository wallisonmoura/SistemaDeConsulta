using SistemaDeConsulta.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Pacientes
{
    public class EditPacienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        [RegularExpression(@"^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$", ErrorMessage = "Formato de telefone inválido. Utilize o formato (99) 99999-9999.")]
        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
