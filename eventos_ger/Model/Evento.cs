using System.Text.Json.Serialization;
using eventos_ger.Model;

public class Evento
{
    public int Id { get; set; }
    public string? data { get; set; }
    public string? nome { get; set; }
    public string? descricao { get; set; }
    public List<Palestrante>? palestrantes_presentes { get; set; } = new();
    public List<Participante>? Participantes { get; set; } = new();
    public int id_local { get; set; }
    public int id_organizador { get; set; }
    
    
}