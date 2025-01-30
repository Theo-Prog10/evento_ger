using System.ComponentModel.DataAnnotations.Schema;
namespace eventos_ger.Model;

public class AssociacaoEventoPessoa
{
    public int Id { get; set; }
    
    [ForeignKey("Evento")]
    public int idEvento { get; set; }
    public virtual Evento Evento { get; set; } // Propriedade de navegação

    [ForeignKey("Pessoa")]
    public int idPessoa { get; set; }
    public virtual Pessoa Pessoa { get; set; } // Propriedade de navegação
    public string tipo_pessoa { get; set; }
    
}