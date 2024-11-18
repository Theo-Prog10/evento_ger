﻿using Microsoft.AspNetCore.Mvc;
using eventos_ger.Model;
using Microsoft.EntityFrameworkCore;

namespace eventos_ger.Controller;
[ApiController]

public class EventoController : ControllerBase
{
    private readonly Ger_Evento_Bd _context;

    public EventoController(Ger_Evento_Bd context)
    {
        _context = context;
    }
    
    // listar eventos
    [HttpGet ("/eventos")]
    public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
    {
        return await _context.Eventos
            .ToListAsync();
    }

    // listar evento por id
    [HttpGet("evento/{id}")]
    public async Task<ActionResult<Evento>> GetEvento(int id)
    {
        var evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == id);

        if (evento == null)
        {
            return NotFound();
        }

        return evento;
    }
    
    // criar evento
    [HttpPost("eventos")]
    public async Task<ActionResult<Evento>> PostEvento(Evento evento)
    {

        // Verifica se o organizador associado existe no banco de dados
        var organizador = await _context.Organizadores
            .FirstOrDefaultAsync(o => o.Id == evento.id_organizador);

        if (organizador == null)
        {
            return NotFound(new { mensagem = "Organizador não encontrado." });
        }

        // Adiciona o evento ao contexto
        _context.Eventos.Add(evento);
        
        // Adiciona evento ao organizador
        organizador.eventos_organizados.Add(evento.Id);

        // Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvento), new { id = evento.Id }, evento);
    }

    // atualizar evento
    [HttpPut("evento/{id}")]
    public async Task<IActionResult> PutEvento(int id, Evento evento)
    {
        if (id != evento.Id)
        {
            return BadRequest();
        }

        _context.Entry(evento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    // apagar evento
    [HttpDelete("evento/{id}")]
    public async Task<IActionResult> DeleteEvento(int id)
    {
        var evento = await _context.Eventos.FindAsync(id);
        if (evento == null)
        {
            return NotFound();
        }

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventoExists(int id)
    {
        return _context.Eventos.Any(e => e.Id == id);
    }
}