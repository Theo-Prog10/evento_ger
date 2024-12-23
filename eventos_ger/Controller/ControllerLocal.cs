using eventos_ger.Model;
using eventos_ger.Model.DTOs;
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

        // lista locais cadastrados
        [HttpGet("locais")]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocais()
        {
            return Ok(await _localService.ObterLocaisAsync());
        }

        // lista local por id
        [HttpGet("local/{id}")]
        public async Task<ActionResult<LocalDTO>> GetLocal(int id)
        {
            var localDto = await _localService.ObterPorIdAsync(id);
            if (localDto == null)
            {
                return NotFound();
            }

            return Ok(localDto);
        }

        // cria local
        [HttpPost("local")]
        public async Task<ActionResult<Local>> PostLocal(LocalDTO localDto)
        {
            var novolocal = await _localService.AdicionarAsync(localDto);
            return CreatedAtAction(nameof(GetLocal), new { id = novolocal.Id }, novolocal);
        }

        // apaga local por id
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

        // atualiza local por id
        [HttpPut("local/{id}")]
        public async Task<IActionResult> PutLocal(int id, LocalDTO localDto)
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
