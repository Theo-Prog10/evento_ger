namespace eventos_ger.Model;

public class Palestrante : Pessoa
{
    public string? biografia { get; set; }
    public string? especialidade { get; set; }
    public List<int> palestras_ministradas { get; set; } = new();
}