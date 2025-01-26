using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eventos_ger.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerInscricao : ControllerBase
    {
        private readonly IInscricaoService _inscricaoService;

        public ControllerInscricao(IInscricaoService inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        /// <summary>
        /// Inscreve um participante em um evento.
        /// </summary>
        [HttpPost("evento/{eventoId}/participantes/{participanteId}")]
        public async Task<IActionResult> AddParticipante(int eventoId, int participanteId)
        {
            try
            {
                var result = await _inscricaoService.AddParticipanteAsync(eventoId, participanteId);
                
                if (result)
                    return Ok(new { mensagem = "Participante inscrito com sucesso." });

                return BadRequest(new { mensagem = "Erro ao inscrever participante." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }

        /// <summary>
        /// Adiciona um palestrante em um evento.
        /// </summary>
        [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
        public async Task<IActionResult> AddPalestrante(int eventoId, int palestranteId)
        {
            try
            {
                var result = await _inscricaoService.AddPalestranteAsync(eventoId, palestranteId);

                if (result)
                    return Ok(new { mensagem = "Palestrante adicionado ao evento com sucesso." });

                return BadRequest(new { mensagem = "Erro ao adicionar palestrante ao evento." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }

        /// <summary>
        /// Remove um participante de um evento.
        /// </summary>
        [HttpDelete("evento/{eventoId}/participantes/{participanteId}")]
        public async Task<IActionResult> DeleteParticipanteEvento(int eventoId, int participanteId)
        {
            try
            {
                var result = await _inscricaoService.DeleteParticipanteEventoAsync(participanteId, eventoId);

                if (result)
                    return NoContent(); // Sucesso sem conteúdo adicional

                return BadRequest(new { mensagem = "Erro ao remover participante do evento." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }

        /// <summary>
        /// Remove um palestrante de um evento.
        /// </summary>
        [HttpDelete("evento/{eventoId}/palestrantes/{palestranteId}")]
        public async Task<IActionResult> DeletePalestranteEvento(int eventoId, int palestranteId)
        {
            try
            {
                var result = await _inscricaoService.DeletePalestranteEventoAsync(palestranteId, eventoId);

                if (result)
                    return NoContent(); // Sucesso sem conteúdo adicional

                return BadRequest(new { mensagem = "Erro ao remover palestrante do evento." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
