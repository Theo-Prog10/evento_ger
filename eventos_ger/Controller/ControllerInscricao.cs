using eventos_ger.Repository.Interfaces;
namespace eventos_ger.Controller;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerInscricao : ControllerBase
{
    private readonly IInscricaoRepository _inscricaoRepository;
    

    public ControllerInscricao(IInscricaoRepository inscricaoRepository)
    {
        _inscricaoRepository = inscricaoRepository;
    }
    
    // inscreve participante em evento
    [HttpPost("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> AddParticipante(int eventoId, int participanteId)
    {
        return Ok(_inscricaoRepository.AddParticipante(eventoId, participanteId));
    }
    
    // adiciona palestrante em evento
    [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> AddPalestrante(int eventoId, int palestranteId)
    {
        return Ok(_inscricaoRepository.AddPalestrante(eventoId, palestranteId));
    }
    // Remove participante do evento
    [HttpDelete("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> DeleteParticipanteEvento(int participanteId, int eventoId)
    {
        return Ok(_inscricaoRepository.DeleteParticipanteEvento(participanteId, eventoId));
    }
    
    // Remove palestrante do evento
    [HttpDelete("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> DeletePalestranteEvento(int palestranteId, int eventoId)
    {
        return Ok(_inscricaoRepository.DeletePalestranteEvento(palestranteId, eventoId));
    }
}