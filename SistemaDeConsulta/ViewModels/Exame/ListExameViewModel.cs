using SistemaDeConsulta.Models.Entities;

namespace SistemaDeConsulta.ViewModels.Exame
{
    public class ListExameViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Observacoes { get; set; }

        public string TipoExame { get; set; }
    }
}
