using eventos_ger.Model.DTOs;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ParticipanteController : ControllerBase
{
    private readonly IParticipanteService _participanteService;

    public ParticipanteController(IParticipanteService participanteService)
    {
        _participanteService = participanteService;
    }

    [HttpGet("participantes")]
    public async Task<ActionResult<IEnumerable<ParticipanteDTO>>> GetParticipantes()
    {
        var participantes = await _participanteService.ObterTodosAsync();
        return Ok(participantes);
    }

    [HttpGet("participante/{id}")]
    public async Task<ActionResult<ParticipanteDTO>> GetParticipante(int id)
    {
        var participante = await _participanteService.ObterPorIdAsync(id);
        if (participante == null) return NotFound();
        return Ok(participante);
    }

    [HttpPost("participante")]
    public async Task<ActionResult<ParticipanteDTO>> PostParticipante(ParticipanteDTO participanteDTO)
    {
        var criado = await _participanteService.CriarAsync(participanteDTO);
        return CreatedAtAction(nameof(GetParticipante), new { id = criado.Id }, criado);
    }

    [HttpPut("participante/{id}")]
    public async Task<IActionResult> PutParticipante(int id, ParticipanteDTO participanteDTO)
    {
        if (id != participanteDTO.Id) return BadRequest();

        try
        {
            await _participanteService.AtualizarAsync(id, participanteDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("participante/{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        try
        {
            await _participanteService.RemoverAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}