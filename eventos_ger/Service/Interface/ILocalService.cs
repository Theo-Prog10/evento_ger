using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface;

public interface ILocalService
{
    Task<IEnumerable<LocalDTOResponse>> ObterLocaisAsync();
    Task<LocalDTOResponse> ObterPorIdAsync(int id);
    Task<LocalDTOResponse> AdicionarAsync(LocalDTORequest localDtoRequest);
    Task<LocalDTOResponse> AtualizarAsync(int id, LocalDTORequest localDtoRequest);
    Task DeletarAsync(int id);
}