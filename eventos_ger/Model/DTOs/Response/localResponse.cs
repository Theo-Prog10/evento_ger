namespace eventos_ger.Model.DTOs.Response
{
    public class LocalDTOResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string UF { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty; 
        public string Bairro { get; set; } = string.Empty;
    }
}