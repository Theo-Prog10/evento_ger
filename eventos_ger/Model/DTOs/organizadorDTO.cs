namespace eventos_ger.Model.DTOs;


public class OrganizadorDTO : pessoaDTO
{
    public string? Contato { get; set; } 
    public List<int> EventosOrganizados { get; set; } = new();
}

