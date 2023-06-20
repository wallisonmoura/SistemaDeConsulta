using SistemaDeConsulta.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Exames
{
    public class CreateExameViewModel
    {
        public string Nome { get; set; } = string.Empty;

        public string Observacoes { get; set; } = string.Empty;

        [Display(Name = "Tipo de exame")]
        public int TipoExameId { get; set; }

        public TipoExame TipoExame { get; set; }
    }
}
