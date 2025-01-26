using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface;

public interface ILocalService
{
    // Obter todos os locais
    Task<IEnumerable<LocalDTOResponse>> ObterLocaisAsync();

    // Obter local por ID
    Task<LocalDTOResponse> ObterPorIdAsync(int id);

    // Adicionar novo local
    Task<LocalDTOResponse> AdicionarAsync(LocalDTORequest localDtoRequest);

    // Atualizar local
    Task<LocalDTOResponse> AtualizarAsync(int id, LocalDTORequest localDtoRequest);

    // Deletar local
    Task DeletarAsync(int id);
}