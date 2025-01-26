using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
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

    // Obter todos os participantes (retorna ParticipanteDTOResponse)
    [HttpGet("participantes")]
    public async Task<ActionResult<IEnumerable<ParticipanteDTOResponse>>> GetParticipantes()
    {
        var participantes = await _participanteService.ObterTodosAsync();
        return Ok(participantes);
    }

    // Obter participante por ID (retorna ParticipanteDTOResponse)
    [HttpGet("participante/{id}")]
    public async Task<ActionResult<ParticipanteDTOResponse>> GetParticipante(int id)
    {
        var participante = await _participanteService.ObterPorIdAsync(id);
        if (participante == null) return NotFound();
        return Ok(participante);
    }

    // Criar participante (usa ParticipanteDTORequest para a entrada e retorna ParticipanteDTOResponse)
    [HttpPost("participante")]
    public async Task<ActionResult<ParticipanteDTOResponse>> PostParticipante(ParticipanteDTORequest participanteDTO)
    {
        var criado = await _participanteService.CriarAsync(participanteDTO);
        return CreatedAtAction(nameof(GetParticipantes), new { nome = criado.Nome }, criado);
    }

    // Atualizar participante (usa ParticipanteDTORequest para a entrada)
    [HttpPut("participante/{id}")]
    public async Task<IActionResult> PutParticipante(int id, ParticipanteDTORequest participanteDTO)
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

    // Remover participante (sem necessidade de DTO, já que o ID é suficiente)
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
