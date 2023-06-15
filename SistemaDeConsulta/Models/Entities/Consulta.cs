namespace SistemaDeConsulta.Models.Entities
{
    public class Consulta
    {
        public int Id { get; set; }

        public int PacientId { get; set; }

        public Paciente Paciente { get; set; }

        public int ExameId { get; set; }

        public Exame Exame { get; set; }

        public DateTime DataHora { get; set; } 

        public string NumeroProtocolo { get; set; } 
    }
}
