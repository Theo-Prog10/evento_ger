using Microsoft.EntityFrameworkCore;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<Evento?> ObterPorIdAsync(int id)
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
        //Busca o evento 
        var eventoExistente = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == evento.Id);

        if (eventoExistente == null)
        {
            throw new ArgumentException("Evento não encontrado.");
        }

        //Atualiza os campos 
        eventoExistente.nome = evento.nome;
        eventoExistente.descricao = evento.descricao;
        eventoExistente.data = evento.data;
        eventoExistente.horario = evento.horario;
        eventoExistente.id_local = evento.id_local;
        eventoExistente.id_organizador = evento.id_organizador;
        
        _context.Eventos.Update(eventoExistente);
        await _context.SaveChangesAsync();
    }


    public async Task<bool>DeletarAsync(int id)
    {
        //Busca o evento
        var evento = await _context.Eventos.FindAsync(id);
        if (evento != null)
        {
            
            // Remove associacao
            var associacoes = await _context.Associacoes
                .Where(a => a.idEvento == id)
                .ToListAsync();
            
            _context.Associacoes.RemoveRange(associacoes);
            
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            
            return true;
        }
        return false;
    }


    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Eventos.AnyAsync(e => e.Id == id);
    }
}