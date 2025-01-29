namespace eventos_ger.Model.DTOs.Response
{
    public class PessoaDTOResponse
    {
        public string? Nome { get; set; }
        public DateOnly? Nascimento { get; set; }
        public string? Cpf { get; set; }
        public List<int> EventosInscritos { get; set; } = new();
        public List<int> EventosPalestrados { get; set; } = new();
        public List<int> EventosOrganizados { get; set; } = new();
        //public string? Login { get; set; }
    }
}