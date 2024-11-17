namespace eventos_ger.Model;

public class Participante
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public string? nascimento { get; set; }
    public string? cpf { get; set; }
    public string? tipo_ingresso { get; set; }
    public string? status_inscricao { get; set; }
    public List<Evento>? Eventos_inscritos { get; set; } = new();
}