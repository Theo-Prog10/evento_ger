using eventos_ger.Model;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerOrganizador : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public ControllerOrganizador(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    [HttpGet("/organizadores")]
    public async Task<ActionResult<IEnumerable<Organizador>>> GetOrganizadores()
    {
        return await _context.Organizadores.Include(p => p.eventos_organizados).ToListAsync();
    }
    
    [HttpPost("organizadores")]
    public async Task<ActionResult<Organizador>> PostOrganizador(Organizador organizador)
    {
        _context.Organizadores.Add(organizador);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrganizadores), new { id = organizador.Id }, organizador);
    }
    
    [HttpPut("organizador/{id}")]
    public async Task<IActionResult> PutOrganizador(int id, Organizador inputOrganizador)
    {
        var organizador = await _context.Organizadores.FindAsync(id);
        if (organizador == null) return NotFound();

        organizador.nome = inputOrganizador.nome;
        organizador.nascimento = inputOrganizador.nascimento;
        organizador.cpf = inputOrganizador.cpf;
        organizador.contato = inputOrganizador.contato;
        

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("organizador/{organizadorId}")]
    public async Task<IActionResult> DeleteOrganizador(int organizadorId)
    {
        var organizador = await _context.Organizadores.FindAsync(organizadorId);
        if (organizador == null) return NotFound();

        _context.Organizadores.Remove(organizador);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    

}