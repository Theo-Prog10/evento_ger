using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using eventos_ger.Model;

public class Evento
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public string? descricao { get; set; }
    public string? data { get; set; }
    public string? horario { get; set; }
    
    [ForeignKey("Local")]
    public int id_local { get; set; }
    public virtual Local Local { get; set; }

    [ForeignKey("Pessoa")]
    public int id_organizador { get; set; }
    public virtual Pessoa Organizador { get; set; }
    
}