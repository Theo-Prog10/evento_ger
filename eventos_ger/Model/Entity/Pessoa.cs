namespace eventos_ger.Model;

public class Pessoa
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public string? nascimento { get; set; }
    public string? cpf { get; set; }
    
    public string? Login { get; set; } // Novo campo para o login
    public string? Senha { get; set; } // Novo campo para a senha
    
    public void SetSenha(string senha)
    {
        Senha = BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool VerificarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, Senha);
    }
}