using Microsoft.AspNetCore.Mvc;

public interface IInscricaoService
{
    Task<bool> AddParticipanteAsync(int eventoId, int participanteId);
    Task<bool> AddPalestranteAsync(int eventoId, int palestranteId);  // Certifique-se de que esta linha esteja presente
    Task<bool> DeleteParticipanteEventoAsync(int participanteId, int eventoId);
    Task<bool> DeletePalestranteEventoAsync(int palestranteId, int eventoId);
}