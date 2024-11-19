namespace eventos_ger.Repository.Interfaces;

public class IInscricaoRepository
{    
    Task<IEnumerable<Evento>> ObterEventosAsync();
    Task<Evento> ObterPorIdAsync(int id);
    Task<Evento> AdicionarAsync(Evento evento);
    Task AtualizarAsync(Evento evento);
    Task DeletarAsync(int id);
    Task<bool> ExisteAsync(int id);
}