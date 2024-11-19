using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerLocal : ControllerBase
{
    private readonly ILocalRepository _localRepository;

    public ControllerLocal(ILocalRepository localRepository)
    {
        _localRepository = localRepository;
    }
    
    // lista locais cadastrados
    [HttpGet("locais")]
    public async Task<ActionResult<IEnumerable<Local>>> GetLocais()
    {
        return Ok(await _localRepository.ObterLocaisAsync());
    }
    
    // lista local por id
    [HttpGet("local/{id}")]
    public async Task<ActionResult<IEnumerable<Local>>> GetLocal(int id)
    {
        
        var local = await _localRepository
            .ObterPorIdAsync(id);

        if (local == null)
        {
            return NotFound();
        }

        var localDto = new LocalDTO
        {
            Id = local.Id,
            Nome = local.nome,
            Logradouro = local.Logradouro,
            Numero = local.Numero,
            UF = local.UF,
            Cidade = local.Cidade,
            Bairro = local.Bairro
        };
        
        return Ok(localDto);
    }
    
    // cria local
    [HttpPost("local")]
    public async Task<ActionResult<Local>> PostLocal(LocalDTO localDto)
    {
        var local = new Local
        {
            nome = localDto.Nome,
            Logradouro = localDto.Logradouro,
            Numero = localDto.Numero,
            UF = localDto.UF,
            Cidade = localDto.Cidade,
            Bairro = localDto.Bairro
        };
        
        var novolocal = await _localRepository.AdicionarAsync(local);

        return CreatedAtAction(nameof(GetLocal), new { id = novolocal.Id }, novolocal);
    }
    
    // apaga local por id
    [HttpDelete("local/{localId}")]
    public async Task<IActionResult> DeleteLocal(int localId)
    {
        var local = await _localRepository.ObterPorIdAsync(localId);
        if (local == null) return NotFound();
        
        await _localRepository.DeletarAsync(local.Id);

        return NoContent();
    }
    
    // atualiza local por id
    [HttpPut("local/{id}")]
    public async Task<IActionResult> PutLocal(int id, LocalDTO localDto)
    {
        
        if (id != localDto.Id)
        {
            return BadRequest(new { mensagem = "IDs não coincidem." });
        }
        
        var localExistente = await _localRepository.ObterPorIdAsync(id);
        if (localExistente == null) return NotFound();

        localExistente.nome = localDto.Nome;
        localExistente.Logradouro = localDto.Logradouro;
        localExistente.Bairro = localDto.Bairro;
        localExistente.Cidade = localDto.Cidade;
        localExistente.UF = localDto.UF;
        localExistente.Numero = localDto.Numero;

        await _localRepository.AtualizarAsync(localExistente);

        return NoContent();
    }
}