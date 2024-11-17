using eventos_ger.Model;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerParticipantes : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public ControllerParticipantes(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    [HttpGet("/participantes")]
    public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
    {
        return await _context.Participantes.Include(p => p.Eventos_inscritos).ToListAsync();
    }
    
    [HttpPost("participantes")]
    public async Task<ActionResult<Participante>> PostPessoa(Participante participante)
    {
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticipantes), new { id = participante.Id }, participante);
    }
    
    

}