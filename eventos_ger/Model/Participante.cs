namespace eventos_ger.Model;

public class Participante : Pessoa
{
    public string? tipo_ingresso { get; set; }
    public string? status_inscricao { get; set; }
    public List<int> Eventos_inscritos { get; set; } = new();
}