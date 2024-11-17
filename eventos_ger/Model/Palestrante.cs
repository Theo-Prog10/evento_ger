namespace eventos_ger.Model;

public class Palestrante
{
    public int id_palestrante { get; set; }
    public string nome { get; set; }
    public string nascimento { get; set; }
    public string cpf { get; set; }
    public string biografia { get; set; }
    public string especialidade { get; set; }
    public List<Evento> palestras_ministradas { get; set; } = new();
}