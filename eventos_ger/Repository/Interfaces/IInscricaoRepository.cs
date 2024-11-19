namespace eventos_ger.Repository.Interfaces;

public interface IInscricaoRepository
{    
    Task<Evento> AddParticipante(int eventoId, int participanteId);
    Task<Evento> AddPalestrante(int eventoId, int palestranteId);
    Task DeleteParticipanteEvento(int participanteId, int eventoId);
    Task DeletePalestranteEvento(int palestranteId, int eventoId);
    
}