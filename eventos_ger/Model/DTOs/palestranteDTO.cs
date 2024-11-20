namespace eventos_ger.Model.DTOs;

public class PalestranteDTO : pessoaDTO
{ 
        public string? Biografia { get; set; } 
        public string? Especialidade { get; set; } 

        public List<int> PalestrasMinistradas { get; set; }
    
}
