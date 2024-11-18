using Microsoft.AspNetCore.Http.HttpResults;

namespace eventos_ger.Controller;

using eventos_ger.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class ControllerInscricao : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public ControllerInscricao(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    // inscreve participante em evento
    [HttpPost("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> AddParticipante(int eventoId, int participanteId)
    {
        // Buscar o evento
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == eventoId);
        
        if (evento == null) return NotFound($"Evento com ID {eventoId} não encontrado.");

        // Buscar o participante
        var participante = await _context.Participantes
            .FirstOrDefaultAsync(p => p.Id == participanteId);
        
        if (participante == null) return NotFound($"Participante com ID {participanteId} não encontrado.");
        if (evento.Participantes.Contains(participante.Id))return Conflict($"Participante com ID {participanteId} já existe.");
        
        // Adicionar o participante ao evento e vice-versa
        evento.Participantes.Add(participante.Id);
        participante.Eventos_inscritos.Add(evento.Id);

        await _context.SaveChangesAsync();
        return Ok("participante adicionado");
    }
    
    // adiciona palestrante em evento
    [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> AddPalestrante(int eventoId, int palestranteId)
    {
        // Buscar o evento
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == eventoId);

        if (evento == null) return NotFound($"Evento com ID {eventoId} não encontrado.");
        
        // Buscar o palestrante
        var palestrante = await _context.Palestrantes
            .FirstOrDefaultAsync(p => p.Id == palestranteId);
        
        if (palestrante == null) return NotFound($"Participante com ID {palestranteId} não encontrado.");
        if (evento.palestrantes_presentes.Contains(palestrante.Id))
            return Conflict($"Participante com ID {palestranteId} já existe.");

        // Adicionar o palestrante ao evento e vice-versa
        evento.palestrantes_presentes.Add(palestrante.Id);
        palestrante.palestras_ministradas.Add(evento.Id);

        await _context.SaveChangesAsync();
        return Ok("palestrante adicionado");
    }
    // Remove participante do evento
    [HttpDelete("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> DeleteParticipanteEvento(int participanteId, int eventoId)
    {
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == eventoId);

        if (evento == null) return NotFound($"Evento com ID {eventoId} não encontrado.");

        var participante = await _context.Participantes
            .FirstOrDefaultAsync(p => p.Id == participanteId);
        
        if (participante == null) return NotFound($"Participante com ID {participanteId} não encontrado.");

        evento.Participantes.Remove(participante.Id);
        participante.Eventos_inscritos.Remove(evento.Id);

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // Remove palestrante do evento
    [HttpDelete("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> DeletePalestranteEvento(int palestranteId, int eventoId)
    {
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == eventoId);

        if (evento == null) return NotFound($"Evento com ID {eventoId} não encontrado.");

        var palestrante = await _context.Palestrantes
            .FirstOrDefaultAsync(p => p.Id == palestranteId);
        
        if (palestrante == null) return NotFound($"Palestrante com ID {palestranteId} não encontrado.");

        evento.palestrantes_presentes.Remove(palestrante.Id);
        palestrante.palestras_ministradas.Remove(evento.Id);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}