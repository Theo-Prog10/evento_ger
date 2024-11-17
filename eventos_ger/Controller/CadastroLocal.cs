using eventos_ger.Model;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class CadastroController : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public CadastroController (Ger_Evento_Bd context)
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
}