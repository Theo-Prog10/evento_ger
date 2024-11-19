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
            // Remover o evento das listas de eventos de palestrantes
            var palestrantes = await _context.Palestrantes
                .Where(p => p.palestras_ministradas.Contains(id)) 
                .ToListAsync();
        
            foreach (var palestrante in palestrantes)
            {
                palestrante.palestras_ministradas.Remove(id); 
            }

            // Remover o evento das listas de eventos de participantes
            var participantes = await _context.Participantes
                .Where(p => p.eventosInscritos.Contains(id)) 
                .ToListAsync();
        
            foreach (var participante in participantes)
            {
                participante.eventosInscritos.Remove(id); 
            }

            
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Eventos.AnyAsync(e => e.Id == id);
    }
}