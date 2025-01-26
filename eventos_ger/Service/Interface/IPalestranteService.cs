using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface;

public interface IPalestranteService
{
    // Obter todos os palestrantes
    Task<IEnumerable<PalestranteDTOResponse>> ObterTodosAsync();

    // Obter palestrante por ID
    Task<PalestranteDTOResponse?> ObterPorIdAsync(int id);

    // Criar palestrante
    Task<PalestranteDTOResponse> CriarAsync(PalestranteDTORequest palestranteDTORequest);

    // Atualizar palestrante
    Task<PalestranteDTOResponse> AtualizarAsync(int id, PalestranteDTORequest palestranteDTORequest);

    // Remover palestrante
    Task RemoverAsync(int id);
}