﻿using eventos_ger.Model.DTOs;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class OrganizadorController : ControllerBase
{
    private readonly IOrganizadorService _organizadorService;

    public OrganizadorController(IOrganizadorService organizadorService)
    {
        _organizadorService = organizadorService;
    }

    [HttpGet("organizadores")]
    public async Task<ActionResult<IEnumerable<OrganizadorDTO>>> GetOrganizadores()
    {
        var organizadores = await _organizadorService.ObterTodosAsync();
        return Ok(organizadores);
    }

    [HttpGet("organizador/{id}")]
    public async Task<ActionResult<OrganizadorDTO>> GetOrganizador(int id)
    {
        var organizador = await _organizadorService.ObterPorIdAsync(id);
        if (organizador == null) return NotFound(new { mensagem = "Organizador não encontrado." });

        return Ok(organizador);
    }

    [HttpPost("organizadores")]
    public async Task<ActionResult<OrganizadorDTO>> PostOrganizador(OrganizadorDTO organizadorDTO)
    {
        var criado = await _organizadorService.CriarAsync(organizadorDTO);
        return CreatedAtAction(nameof(GetOrganizador), new { id = criado.Id }, criado);
    }

    [HttpPut("organizador/{id}")]
    public async Task<IActionResult> PutOrganizador(int id, OrganizadorDTO organizadorDTO)
    {
        try
        {
            await _organizadorService.AtualizarAsync(id, organizadorDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("organizador/{id}")]
    public async Task<IActionResult> DeleteOrganizador(int id)
    {
        try
        {
            await _organizadorService.RemoverAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}