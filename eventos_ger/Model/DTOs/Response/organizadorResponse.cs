namespace eventos_ger.Model.DTOs.Response
{
    public class OrganizadorDTOResponse : PessoaDTOResponse
    {
        public string? Contato { get; set; }
        public List<int> EventosOrganizados { get; set; } = new();
    }
}