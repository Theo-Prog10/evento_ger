namespace eventos_ger.Model;

public class Organizador : Pessoa
{
    public string? contato { get; set; }
    public List<int> EventosOrganizados { get; set; } = new();
}