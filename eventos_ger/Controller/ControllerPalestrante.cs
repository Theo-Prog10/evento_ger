using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
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

    // Obter todos os palestrantes (retorna PalestranteDTOResponse)
    [HttpGet("palestrantes")]
    public async Task<ActionResult<IEnumerable<PalestranteDTOResponse>>> GetPalestrantes()
    {
        var palestrantes = await _palestranteService.ObterTodosAsync();
        return Ok(palestrantes);
    }

    // Obter palestrante por ID (retorna PalestranteDTOResponse)
    [HttpGet("palestrante/{id}")]
    public async Task<ActionResult<PalestranteDTOResponse>> GetPalestrante(int id)
    {
        var palestrante = await _palestranteService.ObterPorIdAsync(id);
        if (palestrante == null) return NotFound(new { mensagem = "Palestrante não encontrado." });
        return Ok(palestrante);
    }

    // Criar palestrante (usa PalestranteDTORequest para a entrada e retorna PalestranteDTOResponse)
    [HttpPost("/palestrantes")]
    public async Task<ActionResult<PalestranteDTOResponse>> PostPalestrante(PalestranteDTORequest palestranteDTO)
    {
        var criado = await _palestranteService.CriarAsync(palestranteDTO);
        return CreatedAtAction(nameof(GetPalestrante), new { Nome = criado.Nome }, criado);
    }

    // Atualizar palestrante (usa PalestranteDTORequest para a entrada)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPalestrante(int id, PalestranteDTORequest palestranteDTO)
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

    // Remover palestrante (sem necessidade de DTO, já que o ID é suficiente)
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
