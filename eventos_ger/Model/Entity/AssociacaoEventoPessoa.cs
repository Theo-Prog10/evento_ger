namespace eventos_ger.Model;

public class AssociacaoEventoPessoa
{
    public int Id { get; set; }
    public int idEvento { get; set; }
    public int idPessoa { get; set; }
    public string tipo_pessoa { get; set; }
    
    public string login_pessoa { get; set; }
}