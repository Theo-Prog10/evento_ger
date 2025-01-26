namespace eventos_ger.Model.DTOs.Request;

public class pessoaRequest
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Nascimento { get; set; }
    public string? Cpf { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
}