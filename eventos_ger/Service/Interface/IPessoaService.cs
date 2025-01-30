using System.Threading.Tasks;
using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface
{
    public interface IPessoaService
    {
        Task<Pessoa?> ValidarLoginAsync(string login, string senha); // Tornando o nome consistente com o método assíncrono
        
        // Obter todos os usuarios
        Task<IEnumerable<PessoaDTOResponse>> ObterTodosAsync();

        // Obter usuario por ID
        Task<PessoaDTOResponse> ObterPorIdAsync(int id);

        // Criar usuario
        Task<PessoaDTOResponse> CriarAsync(PessoaDTORequest usuarioRequestDTO);

        // Atualizar usuario
        Task<PessoaDTOResponse> AtualizarAsync(int id, PessoaDTORequest usuarioRequestDTO);

        // Remover usuario
        Task RemoverAsync(int id);
    }
}