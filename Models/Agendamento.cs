namespace AgendaFacil.Models;

    public class Agendamento {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int EventoId { get; set; }
        public int HorarioId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Codigo { get; set; }
        
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string TituloEvento { get; set; }
        public DateTime DataHora { get; set; }
    }

