namespace eventos_ger.Model.DTOs.Response
{
    public class PessoaDTOResponse
    {
        public int id { get; set; }
        public string? Nome { get; set; }
        public DateOnly? Nascimento { get; set; }
        public string? Cpf { get; set; }
        public string? biografia { get; set; }
        public string? especialidade { get; set; }
        public string? contato { get; set; }
        
        public List<int> EventosInscritos { get; set; } = new();
        public List<int> EventosPalestrados { get; set; } = new();
        public List<int> EventosOrganizados { get; set; } = new();
    }
}