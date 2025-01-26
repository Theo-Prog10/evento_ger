using eventos_ger.Model;
using System.Threading.Tasks;

namespace eventos_ger.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        // Alterado para ser assíncrono e retornar um Task<Pessoa?>
        Task<Pessoa?> ObterPorLoginESenhaAsync(string login, string senha);
    }
}