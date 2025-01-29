namespace eventos_ger.Model.DTOs.Request;

public class PessoaDTORequest
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public DateOnly? Nascimento { get; set; }
    public string? Cpf { get; set; }
    public string? biografia { get; set; }
    public string? especialidade { get; set; }
    public string? contato { get; set; }
    public string? login { get; set; }
    public string? senha { get; set; }
}