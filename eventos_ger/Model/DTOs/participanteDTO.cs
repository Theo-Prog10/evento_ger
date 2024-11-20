namespace eventos_ger.Model.DTOs;

public class ParticipanteDTO : pessoaDTO
{
    public string? Tipo_ingresso { get; set; }
    public string? Status_inscricao { get; set; }

    public List<int> EventosInscritos { get; set; } = new();

}