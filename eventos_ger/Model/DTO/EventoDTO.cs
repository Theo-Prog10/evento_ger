namespace eventos_ger.Model.DTO;

public class EventoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Data { get; set; }
    public int IdLocal { get; set; }
    public List<int> ParticipantesIds { get; set; } // Lista de IDs dos participantes
}