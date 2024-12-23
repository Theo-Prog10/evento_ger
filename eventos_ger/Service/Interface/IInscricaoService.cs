using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Service.Interface;

public interface IInscricaoService
{
    Task<IActionResult> AddParticipanteAsync(int eventoId, int participanteId);
    Task<IActionResult> AddPalestranteAsync(int eventoId, int palestranteId);
    Task<IActionResult> DeleteParticipanteEventoAsync(int participanteId, int eventoId);
    Task<IActionResult> DeletePalestranteEventoAsync(int palestranteId, int eventoId);
}