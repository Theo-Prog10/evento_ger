namespace eventos_ger.Model.DTOs;

public class ParticipanteDTO
{
    public int Id { get; set; } 
    public string Nome { get; set; } = string.Empty; 
    public string? nascimento { get; set; } 
    
    public string? cpf { get; set; }
    public string? tipo_ingresso { get; set; }
    public string? status_inscricao { get; set; }

    public List<int> EventosInscritos { get; set; } = new(); 
    
}