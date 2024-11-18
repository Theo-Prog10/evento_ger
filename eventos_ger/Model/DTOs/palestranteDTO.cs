namespace eventos_ger.Model.DTOs;

public class PalestranteDTO
{ 
        public int Id { get; set; } 
        public string Nome { get; set; } = string.Empty; 
        public string? Biografia { get; set; } 
        public string? Especialidade { get; set; } 
        
        public string? Nascimento { get; set; } 
        
        public string? cpf { get; set; }

        public List<int> PalestrasMinistradas { get; set; } = new();
    
}
