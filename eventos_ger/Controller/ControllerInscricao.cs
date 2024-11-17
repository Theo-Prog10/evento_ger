using Microsoft.AspNetCore.Http.HttpResults;

namespace eventos_ger.Controller;

using eventos_ger.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class ControllerInscricao
{
    private readonly Ger_Evento_Bd _context;

    public ControllerInscricao(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    // inscreve participante em evento
    [HttpPost("{eventoId}/participantes/{participanteId}")]
    public async Task AddParticipante(int eventoId, int participanteId)
    {
        // Buscar o evento
        var evento = await _context.Eventos
            .Include(e => e.Participantes)
            .FirstOrDefaultAsync(e => e.Id == eventoId);
        

        // Buscar o participante
        var participante = await _context.Participantes
            .Include(p => p.Eventos_inscritos)
            .FirstOrDefaultAsync(p => p.Id == participanteId);
        
        

        // Adicionar o participante ao evento e vice-versa
        evento.Participantes.Add(participante);
        participante.Eventos_inscritos.Add(evento);

        await _context.SaveChangesAsync();
        
    }
    
    // adiciona palestrante em evento
    [HttpPost("{eventoId}/palestrantes/{palestranteId}")]
    public async Task AddPalestrante(int eventoId, int palestranteId)
    {
        // Buscar o evento
        var evento = await _context.Eventos
            .Include(e => e.palestrantes_presentes)
            .FirstOrDefaultAsync(e => e.Id == eventoId);


        // Buscar o palestrante
        var palestrante = await _context.Palestrantes
            .Include(p => p.palestras_ministradas)
            .FirstOrDefaultAsync(p => p.Id == palestranteId);

        // Adicionar o palestrante ao evento e vice-versa
        evento.palestrantes_presentes.Add(palestrante);
        palestrante.palestras_ministradas.Add(evento);

        await _context.SaveChangesAsync();
        
    }
}