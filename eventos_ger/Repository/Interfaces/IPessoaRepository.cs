using eventos_ger.Model;
using System.Threading.Tasks;

namespace eventos_ger.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa> ObterPorIdAsync(int id);
        Task<Pessoa> ObterPorLoginAsync(string login);
        Task<IEnumerable<Pessoa>> ObterTodosAsync();
        Task<Pessoa> AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(Pessoa pessoa);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);

        Task<Pessoa?> ObterPorLoginSenhaAsync(string login, string senha);
    }
}