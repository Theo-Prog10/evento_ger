using eventos_ger.Model.DTOs;

namespace eventos_ger.Service.Interface;

public interface IPalestranteService
{
    Task<IEnumerable<PalestranteDTO>> ObterTodosAsync();
    Task<PalestranteDTO?> ObterPorIdAsync(int id);
    Task<PalestranteDTO> CriarAsync(PalestranteDTO palestranteDTO);
    Task AtualizarAsync(int id, PalestranteDTO palestranteDTO);
    Task RemoverAsync(int id);
}