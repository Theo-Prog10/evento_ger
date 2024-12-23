using eventos_ger.Model.DTOs;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerPalestrante : ControllerBase
{
    private readonly IPalestranteService _palestranteService;

    public ControllerPalestrante(IPalestranteService palestranteService)
    {
        _palestranteService = palestranteService;
    }

    [HttpGet("palestrantes")]
    public async Task<ActionResult<IEnumerable<PalestranteDTO>>> GetPalestrantes()
    {
        var palestrantes = await _palestranteService.ObterTodosAsync();
        return Ok(palestrantes);
    }

    [HttpGet("palestrante/{id}")]
    public async Task<ActionResult<PalestranteDTO>> GetPalestrante(int id)
    {
        var palestrante = await _palestranteService.ObterPorIdAsync(id);
        if (palestrante == null) return NotFound(new { mensagem = "Palestrante não encontrado." });
        return Ok(palestrante);
    }

    [HttpPost("/palestrantes")]
    public async Task<ActionResult<PalestranteDTO>> PostPalestrante(PalestranteDTO palestranteDTO)
    {
        var criado = await _palestranteService.CriarAsync(palestranteDTO);
        return CreatedAtAction(nameof(GetPalestrante), new { id = criado.Id }, criado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPalestrante(int id, PalestranteDTO palestranteDTO)
    {
        try
        {
            await _palestranteService.AtualizarAsync(id, palestranteDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePalestrante(int id)
    {
        try
        {
            await _palestranteService.RemoverAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}