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
    
    // lista palestrantes
    [HttpGet("/palestrantes")]
    public async Task<ActionResult<IEnumerable<Palestrante>>> GetPalestrantes()
    {
        return await _context.Palestrantes.ToListAsync();
    }
    
    // Lista palestrante por id
    [HttpGet("/palestrante/{id}")]
    public async Task<ActionResult<Organizador>> GetPalestrante(int id)
    {
        // Busca o palestrante pelo ID, incluindo palestras_ministradas
        var palestrante = await _context.Palestrantes
            .FirstOrDefaultAsync(p => p.Id == id);

        if (palestrante == null)
        {
            return NotFound(new { mensagem = "Palestrante não encontrado." });
        }

        return Ok(palestrante);
    }
    
    // cria palestrante
    [HttpPost("palestrantes")]
    public async Task<ActionResult<Palestrante>> PostPalestrante(Palestrante palestrante)
    {
        _context.Palestrantes.Add(palestrante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPalestrantes), new { id = palestrante.Id }, palestrante);
    }
    
    // atualiza palestrante por id
    [HttpPut("palaestrante/{id}")]
    public async Task<IActionResult> PutPalestrante(int id, Palestrante inputPalestrante)
    {
        var palestrante = await _context.Palestrantes.FindAsync(id);
        if (palestrante == null) return NotFound();

        palestrante.nome = inputPalestrante.nome;
        palestrante.nascimento = inputPalestrante.nascimento;
        palestrante.cpf = inputPalestrante.cpf;
        palestrante.biografia = inputPalestrante.biografia;
        palestrante.especialidade = inputPalestrante.especialidade;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // apaga palestrante
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