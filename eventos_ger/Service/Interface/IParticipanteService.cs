using eventos_ger.Model.DTOs;

namespace eventos_ger.Service.Interface;

public interface IParticipanteService
{
    Task<IEnumerable<ParticipanteDTO>> ObterTodosAsync();
    Task<ParticipanteDTO> ObterPorIdAsync(int id);
    Task<ParticipanteDTO> CriarAsync(ParticipanteDTO participanteDTO);
    Task AtualizarAsync(int id, ParticipanteDTO participanteDTO);
    Task RemoverAsync(int id);
}