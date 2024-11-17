using System.Text.Json.Serialization;
using eventos_ger.Model;

public class Evento
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public string? descricao { get; set; }
    public int id_palestrante { get; set; }
    public List<Participante>? Participantes { get; set; } = new();
    public Local local { get; set; }
    
    
}