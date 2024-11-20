using eventos_ger.Model;

namespace eventos_ger.Repository.Interfaces;

public interface IAssociacaoEventoPessoa
{
    Task<List<int>> ObterEventosAsync(int idPessoa, string tipo_pessoa);
    Task<List<int>> ObterPessoasAsync(int idEvento, string tipo_pessoa);
    
    Task<AssociacaoEventoPessoa> AdicionarAsync(AssociacaoEventoPessoa associacao);
}