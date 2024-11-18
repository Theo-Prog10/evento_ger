using eventos_ger.Model;
using Microsoft.EntityFrameworkCore;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Repository;

public class ParticipanteRepository : IParticipanteRepository
{
    private readonly Ger_Evento_Bd _context;

    public ParticipanteRepository(Ger_Evento_Bd context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Participante>> ObterTodosAsync()
    {
        return await _context.Participantes.ToListAsync();
    }

    public async Task<Participante?> ObterPorIdAsync(int id)
    {
        return await _context.Participantes
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Participante> AdicionarAsync(Participante participante)
    {
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();
        return participante;
    }

    public async Task AtualizarAsync(Participante participante)
    {
        _context.Participantes.Update(participante);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(int id)
    {
        // Busca o participante pelo ID
        var participante = await _context.Participantes.FindAsync(id);

        if (participante != null)
        {
            // Carrega os eventos associados
            var eventos = await _context.Eventos
                .Where(e => e.Participantes.Contains(id))
                .ToListAsync();

            
            foreach (var evento in eventos)
            {
                evento.Participantes.Remove(id);
            }

            
            await _context.SaveChangesAsync();

            // Remove o participante do banco de dados
            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Participantes.AnyAsync(p => p.Id == id);
    }
}