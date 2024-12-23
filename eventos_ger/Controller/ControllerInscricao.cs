
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller
{
    [ApiController]
    public class ControllerInscricao : ControllerBase
    {
        private readonly IInscricaoService _inscricaoService;

        public ControllerInscricao(IInscricaoService inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        // inscreve participante em evento
        [HttpPost("evento/{eventoId}/participantes/{participanteId}")]
        public async Task<IActionResult> AddParticipante(int eventoId, int participanteId)
        {
            return await _inscricaoService.AddParticipanteAsync(eventoId, participanteId);
        }

        // adiciona palestrante em evento
        [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
        public async Task<IActionResult> AddPalestrante(int eventoId, int palestranteId)
        {
            return await _inscricaoService.AddPalestranteAsync(eventoId, palestranteId);
        }

        // remove participante do evento
        [HttpDelete("evento/{eventoId}/participantes/{participanteId}")]
        public async Task<IActionResult> DeleteParticipanteEvento(int participanteId, int eventoId)
        {
            return await _inscricaoService.DeleteParticipanteEventoAsync(participanteId, eventoId);
        }

        // remove palestrante do evento
        [HttpDelete("evento/{eventoId}/palestrantes/{palestranteId}")]
        public async Task<IActionResult> DeletePalestranteEvento(int palestranteId, int eventoId)
        {
            return await _inscricaoService.DeletePalestranteEventoAsync(palestranteId, eventoId);
        }
    }
}