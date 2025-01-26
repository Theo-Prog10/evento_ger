namespace eventos_ger.Model.DTOs.Request
{
    public class ParticipanteDTORequest : pessoaRequest
    {
        public string? Tipo_ingresso { get; set; }
        public string? Status_inscricao { get; set; }
        
    }
}