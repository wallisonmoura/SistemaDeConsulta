using SistemaDeConsulta.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Consultas
{
    public class CreateConsultaViewModel
    {
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente Pacientes { get; set; }

        [Display(Name = "Exame")]
        public int ExameId { get; set; }
        public Exame Exames { get; set; }

        [Display(Name = "Data / Hora")]
        public DateTime DataHora { get; set; }

        [Display(Name = "Número de protocolo")]
        public string NumeroProtocolo { get; set; }
    }
}
