namespace SistemaDeConsulta.Models.Entities
{
    public class Exame
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Observacoes { get; set; }

        public int TipoExameId { get; set; }

        public TipoExame TipoExame { get; set; }

    }
}
