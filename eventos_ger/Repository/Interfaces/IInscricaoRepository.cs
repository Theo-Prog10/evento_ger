namespace eventos_ger.Repository.Interfaces;

public interface IInscricaoRepository
{    
    Task<Evento> AddParticipanteAsync(int eventoId, int participanteId);
    Task<Evento> AddPalestranteAsync(int eventoId, int palestranteId);
    Task DeleteParticipanteEventoAsync(int participanteId, int eventoId);
    Task DeletePalestranteEventoAsync(int palestranteId, int eventoId);
    
}