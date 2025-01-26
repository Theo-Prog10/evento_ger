namespace eventos_ger.Model.DTOs.Response
{
    public class PalestranteDTOResponse : PessoaDTOResponse
    {
        public string? Biografia { get; set; }
        public string? Especialidade { get; set; }

        public List<int> PalestrasMinistradas { get; set; } = new();
    }
}