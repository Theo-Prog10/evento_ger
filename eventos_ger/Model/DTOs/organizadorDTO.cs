namespace eventos_ger.Model.DTOs;


public class OrganizadorDTO
{
    public int Id { get; set; } 
    public string Nome { get; set; } = string.Empty; 
    public string? Contato { get; set; } 
    public List<string> EventosOrganizados { get; set; } = new();
}

