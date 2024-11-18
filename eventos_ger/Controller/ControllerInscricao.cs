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
        

        // Buscar o participante
        var participante = await _context.Participantes
            .FirstOrDefaultAsync(p => p.Id == participanteId);
        
        // Adicionar o participante ao evento e vice-versa
        evento.Participantes.Add(participante.Id);
        participante.Eventos_inscritos.Add(evento.Id);

        await _context.SaveChangesAsync();
        return Ok("participante adicionado");
    }
    
    // adiciona palestrante em evento
    [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task AddPalestrante(int eventoId, int palestranteId)
    {
        // Buscar o evento
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == eventoId);


        // Buscar o palestrante
        var palestrante = await _context.Palestrantes
            .FirstOrDefaultAsync(p => p.Id == palestranteId);

        // Adicionar o palestrante ao evento e vice-versa
        evento.palestrantes_presentes.Add(palestrante.Id);
        palestrante.palestras_ministradas.Add(evento.Id);

        await _context.SaveChangesAsync();
        
    }
}