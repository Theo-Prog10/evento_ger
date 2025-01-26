using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller
{
    [ApiController]
    public class ControllerLocal : ControllerBase
    {
        private readonly ILocalService _localService;

        public ControllerLocal(ILocalService localService)
        {
            _localService = localService;
        }

        // Lista locais cadastrados (retorna LocalDTOResponse)
        [HttpGet("locais")]
        public async Task<ActionResult<IEnumerable<LocalDTOResponse>>> GetLocais()
        {
            var locais = await _localService.ObterLocaisAsync();
            return Ok(locais);
        }

        // Lista local por id (retorna LocalDTOResponse)
        [HttpGet("local/{id}")]
        public async Task<ActionResult<LocalDTOResponse>> GetLocal(int id)
        {
            var localDto = await _localService.ObterPorIdAsync(id);
            if (localDto == null)
            {
                return NotFound();
            }

            return Ok(localDto);
        }

        // Cria local (usa LocalDTORequest para entrada e retorna LocalDTOResponse)
        [HttpPost("local")]
        public async Task<ActionResult<LocalDTOResponse>> PostLocal(LocalDTORequest localDto)
        {
            var novoLocal = await _localService.AdicionarAsync(localDto);
            return CreatedAtAction(nameof(GetLocal), new { id = novoLocal.Id }, novoLocal);
        }

        // Apaga local por id
        [HttpDelete("local/{localId}")]
        public async Task<IActionResult> DeleteLocal(int localId)
        {
            try
            {
                await _localService.DeletarAsync(localId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Atualiza local por id (usa LocalDTORequest para entrada)
        [HttpPut("local/{id}")]
        public async Task<IActionResult> PutLocal(int id, LocalDTORequest localDto)
        {
            if (id != localDto.Id)
            {
                return BadRequest(new { mensagem = "IDs não coincidem." });
            }

            try
            {
                await _localService.AtualizarAsync(id, localDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
