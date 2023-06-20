using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.ViewModels.Consultas
{
    public class ListConsultaViewModel
    {
        public int Id { get; set; }

        public string PacienteNome { get; set; }

        public string ExameNome { get; set; }

        public DateTime DataHora { get; set; }

        public string NumeroProtocolo { get; set; }
    }
}
