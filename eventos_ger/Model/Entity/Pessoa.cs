using System.ComponentModel.DataAnnotations.Schema;
namespace eventos_ger.Model;

public class Pessoa
{
    public int Id { get; set; }
    public string? nome { get; set; }
    public DateOnly? nascimento { get; set; }
    public string? cpf { get; set; }
    public string? biografia { get; set; }
    public string? especialidade { get; set; }
    public string? contato { get; set; }
    
    [ForeignKey("Usuario")]
    public int id_usuario { get; set; }
    public virtual Usuario Usuario { get; set; }
    
}