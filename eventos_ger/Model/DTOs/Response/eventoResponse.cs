namespace eventos_ger.Model.DTOs.Response
{
    public class EventoDTOResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Data { get; set; }
        public string? Horario { get; set; }
        public int IdLocal { get; set; }
        public int IdOrganizador { get; set; }

        public List<int> Palestrantes { get; set; } = new();
        public List<int> Participantes { get; set; } = new();
    }
}