namespace eventos_ger.Model.DTOs;

public class PalestranteDTO
{ 
        public int Id { get; set; } 
        public string Nome { get; set; } = string.Empty; 
        public string? Biografia { get; set; } 
        public string? Especialidade { get; set; } 
        public List<string> PalestrasMinistradas { get; set; } = new();
    
}
