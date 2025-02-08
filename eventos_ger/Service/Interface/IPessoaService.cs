using System.Threading.Tasks;
using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface
{
    public interface IPessoaService
    {
        Task<PessoaDTOResponse?> ValidarLoginAsync(string login, string senha);
        Task<IEnumerable<PessoaDTOResponse>> ObterTodosAsync();
        Task<IEnumerable<PessoaDTOResponse>> ObterPessoasEvento(int id, string tipo_pessoa);
        Task<PessoaDTOResponse> ObterPorIdAsync(int id);
        Task<PessoaDTOResponse> ObterPorLoginAsync(string login);
        Task<PessoaDTOResponse> CriarAsync(PessoaDTORequest usuarioRequestDTO);
        Task<PessoaDTOResponse> AtualizarAsync(int id, PessoaDTORequest usuarioRequestDTO);
        Task RemoverAsync(int id);
    }
}