using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface;

public interface IParticipanteService
{
    // Obter todos os participantes
    Task<IEnumerable<ParticipanteDTOResponse>> ObterTodosAsync();

    // Obter participante por ID
    Task<ParticipanteDTOResponse> ObterPorIdAsync(int id);

    // Criar participante
    Task<ParticipanteDTOResponse> CriarAsync(ParticipanteDTORequest participanteRequestDTO);

    // Atualizar participante
    Task<ParticipanteDTOResponse> AtualizarAsync(int id, ParticipanteDTORequest participanteRequestDTO);

    // Remover participante
    Task RemoverAsync(int id);
}