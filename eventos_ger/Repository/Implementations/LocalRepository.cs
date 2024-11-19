using Microsoft.EntityFrameworkCore;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Repository;

public class LocalRepository : ILocalRepository
{
    private readonly Ger_Evento_Bd _context;

    public LocalRepository(Ger_Evento_Bd context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Local>> ObterLocaisAsync()
    {
        return await _context.Locais.ToListAsync();
    }
    
    public async Task<Local> ObterPorIdAsync(int id)
    {
        return await _context.Locais.FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public async Task<Local> AdicionarAsync(Local local)
    {
        _context.Locais.Add(local);
        await _context.SaveChangesAsync();
        return local;
    }
    
    public async Task AtualizarAsync(Local local)
    {
        _context.Entry(local).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        // Busca o local que será deletado
        var local = await _context.Locais.FindAsync(id);
        if (local != null)
        {
            // Remover o local das listas de eventos
            var eventos = await _context.Eventos
                .Where(p => p.id_local.Equals(id)) 
                .ToListAsync();

            foreach (var evento in eventos)
            {
                evento.id_local = 0;
            }
            
            _context.Locais.Remove(local);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Eventos.AnyAsync(e => e.Id == id);
    }
}