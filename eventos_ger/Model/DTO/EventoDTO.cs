namespace eventos_ger.Model.DTO;


public class EventoDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Data { get; set; }
    public int IdLocal { get; set; }
    public int IdOrganizador { get; set; }

    public List<int>? Palestrantes { get; set; } 
    public List<int>? Participantes { get; set; } 
    
}
