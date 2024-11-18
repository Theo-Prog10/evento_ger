namespace eventos_ger.Model.DTOs;


public class EventoDTO
{ 
    public int Id { get; set; } 
    public string Nome { get; set; } = string.Empty; 
    public string? Descricao { get; set; } 
    public string? Data { get; set; } 
    public string? Horario { get; set; } 
    public string NomeLocal { get; set; } = string.Empty; 
    public string NomeOrganizador { get; set; } = string.Empty; 
                    
    
    public List<string> Palestrantes { get; set; } = new(); 
    public List<string> Participantes { get; set; } = new();
    
}
