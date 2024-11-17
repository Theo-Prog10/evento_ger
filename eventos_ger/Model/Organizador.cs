namespace eventos_ger.Model;

public class Organizador
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public string? nascimento { get; set; }
    public string? cpf { get; set; }
    public string? contato { get; set; }
    public List<Evento>? eventos_organizados { get; set; } = new();
}