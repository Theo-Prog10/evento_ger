using eventos_ger.Model;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerPalestrantes : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public ControllerPalestrantes(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    [HttpGet("/palestrantes")]
    public async Task<ActionResult<IEnumerable<Palestrante>>> GetPalestrantes()
    {
        return await _context.Palestrantes.Include(p => p.palestras_ministradas).ToListAsync();
    }
    
    [HttpPost("palestrantes")]
    public async Task<ActionResult<Palestrante>> PostPalestrante(Palestrante palestrante)
    {
        _context.Palestrantes.Add(palestrante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPalestrantes), new { id = palestrante.Id }, palestrante);
    }
    
    [HttpPut("palaestrante/{id}")]
    public async Task<IActionResult> PutPalestrante(int id, Palestrante inputPalestrante)
    {
        var palestrante = await _context.Palestrantes.FindAsync(id);
        if (palestrante == null) return NotFound();

        palestrante.Nome = inputPalestrante.Nome;
        palestrante.nascimento = inputPalestrante.nascimento;
        palestrante.cpf = inputPalestrante.cpf;
        palestrante.biografia = inputPalestrante.biografia;
        palestrante.especialidade = inputPalestrante.especialidade;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("palestrante/{participanteId}")]
    public async Task<IActionResult> DeletePalestrnate(int palestranteId)
    {
        var palestrante = await _context.Participantes.FindAsync(palestranteId);
        if (palestrante == null) return NotFound();

        _context.Participantes.Remove(palestrante);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    

}