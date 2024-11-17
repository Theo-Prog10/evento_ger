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
    
    // lista participantes
    [HttpGet("/participantes")]
    public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
    {
        return await _context.Participantes.Include(p => p.Eventos_inscritos).ToListAsync();
    }
    
    // Lista participante por id
    [HttpGet("/participante/{id}")]
    public async Task<ActionResult<Participante>> GetParticipante(int id)
    {
        // Busca o participante pelo ID, incluindo Eventos_inscritos
        var participante = await _context.Participantes
            .Include(p => p.Eventos_inscritos) // Inclui Eventos_inscritos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (participante == null)
        {
            return NotFound(new { mensagem = "Participante não encontrado." });
        }

        return Ok(participante);
    }
    
    // cria participante
    [HttpPost("participantes")]
    public async Task<ActionResult<Participante>> PostPessoa(Participante participante)
    {
        _context.Participantes.Add(participante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticipantes), new { id = participante.Id }, participante);
    }
    
    // atualiza dados participante
    [HttpPut("participante/{id}")]
    public async Task<IActionResult> PutParticipante(int id, Participante inputParticipante)
    {
        var participante = await _context.Participantes.FindAsync(id);
        if (participante == null) return NotFound();

        participante.nome = inputParticipante.nome;
        participante.nascimento = inputParticipante.nascimento;
        participante.cpf = inputParticipante.cpf;
        participante.status_inscricao = inputParticipante.status_inscricao;
        participante.tipo_ingresso = inputParticipante.tipo_ingresso;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // apaga participante
    [HttpDelete("participante/{participanteId}")]
    public async Task<IActionResult> DeleteParticipante(int participanteId)
    {
        var participante = await _context.Participantes.FindAsync(participanteId);
        if (participante == null) return NotFound();

        _context.Participantes.Remove(participante);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("participante/{participanteId}/eventos/{eventoId}")]
    public async Task<IActionResult> DeleteEvento(int participanteId, int eventoId)
    {
        var evento = await _context.Eventos
            .Include(e => e.Participantes)
            .FirstOrDefaultAsync(e => e.Id == eventoId);

        if (evento == null) return NotFound();

        var participante = evento.Participantes.FirstOrDefault(p => p.Id == participanteId);
        if (participante == null) return NotFound();

        evento.Participantes.Remove(participante);

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}