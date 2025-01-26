using System.Threading.Tasks;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface
{
    public interface IPessoaService
    {
        Task<LoginResponse> ValidarLoginAsync(string login, string senha); // Tornando o nome consistente com o método assíncrono
    }
}