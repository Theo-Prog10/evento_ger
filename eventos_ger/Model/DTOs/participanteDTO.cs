namespace eventos_ger.Model.DTOs;

public class ParticipanteDTO
{
    public int Id { get; set; } 
    public string Nome { get; set; } = string.Empty; 
    public string? Nascimento { get; set; }

    
    public List<string> EventosInscritos { get; set; } = new(); 
    
}