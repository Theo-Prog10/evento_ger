using eventos_ger.Model;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerLocal : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public ControllerLocal (Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    [HttpGet("locais")]
    public async Task<ActionResult<IEnumerable<Local>>> GetLocal()
    {
        return await _context.Locais.ToListAsync();
    }
    
    [HttpPost("local")]
    public async Task<ActionResult<Local>> PostLocal(Local local)
    {
        _context.Locais.Add(local);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLocal), new { id = local.Id }, local);
    }
    
    [HttpDelete("locais/{localId}")]
    public async Task<IActionResult> DeleteLocal(int localId)
    {
        var local = await _context.Locais.FindAsync(localId);
        if (local == null) return NotFound();

        _context.Locais.Remove(local);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut("locais/{id}")]
    public async Task<IActionResult> PutLocal(int id, Local inputLocal)
    {
        var local = await _context.Locais.FindAsync(id);
        if (local == null) return NotFound();

        local.Logradouro = inputLocal.Logradouro;
        local.Bairro = inputLocal.Bairro;
        local.Cidade = inputLocal.Cidade;
        local.UF = inputLocal.UF;
        local.Numero = inputLocal.Numero;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}