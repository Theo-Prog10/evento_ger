using Microsoft.EntityFrameworkCore;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Repository;

public class EventoRepository : IEventoRepository
{
    private readonly Ger_Evento_Bd _context;

    public EventoRepository(Ger_Evento_Bd context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Evento>> ObterEventosAsync()
    {
        return await _context.Eventos.ToListAsync();
    }

    public async Task<Evento> ObterPorIdAsync(int id)
    {
        return await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
    }
    

    public async Task<Evento> AdicionarAsync(Evento evento)
    {
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task AtualizarAsync(Evento evento)
    {
        _context.Entry(evento).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        // Busca o evento que será deletado
        var evento = await _context.Eventos.FindAsync(id);
        if (evento != null)
        {
            
            // Remover associacao
            var associacoes = await _context.Associacoes
                .Where(a => a.idEvento == id)
                .ToListAsync();
            
            _context.Associacoes.RemoveRange(associacoes);
            
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Eventos.AnyAsync(e => e.Id == id);
    }
}