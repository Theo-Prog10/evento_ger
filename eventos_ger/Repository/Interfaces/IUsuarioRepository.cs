using eventos_ger.Model;
namespace eventos_ger.Repository.Interfaces;

public interface IUsuarioRepository
{
    // Alterado para ser assíncrono e retornar um Task<Pessoa?>
    Task<Pessoa?> ObterPorLoginESenhaAsync(string login, string senha);
    Task<IEnumerable<string>> ObterLoginsAsync();
    Task<string> ObterLoginAsync(int id);
    Task<Usuario> AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task DeletarAsync(int id);
    Task<bool> ExisteAsync(int id);
}