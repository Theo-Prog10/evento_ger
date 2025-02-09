using eventos_ger.Service;
using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller
{
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet("/eventos")]
        public async Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventos()
        {
            var eventos = await _eventoService.GetEventos();
            return (eventos);
        }

        // Listar eventos associados a pessoa
        [HttpGet("/eventosPessoa/{login}/{tipo_pessoa}")]
        public async Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventosPessoa(string login, string tipo_pessoa)
        {
            var eventos = await _eventoService.GetEventosPessoa(login, tipo_pessoa);
            return (eventos);
        }

        // Listar evento por id
        [HttpGet("evento/{id}")]
        public async Task<ActionResult<EventoDTOResponse>> GetEvento(int id)
        {
            var evento = await _eventoService.GetEvento(id);
            if (evento == null)
                return NotFound(new { mensagem = "Evento não encontrado." });

            return (evento);
        }
        
        // Listar participantes por evento
        [HttpGet("evento/{id}/{login}/{tipo_pessoa}")]
        public async Task<ActionResult<EventoDTOResponse>> GetPessoasEvento(int id, string login, string tipo_pessoa)
        {
            var evento = await _eventoService.GetEvento(id);
            if (evento == null)
                return NotFound(new { mensagem = "Evento não encontrado." });
            //var pessoas = await _

            return (evento);
        }

        // Criar evento
        [HttpPost("eventos")]
        public async Task<ActionResult<EventoDTOResponse>> PostEvento([FromBody] EventoDTORequest eventoRequestDTO)
        {
            if (eventoRequestDTO == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            var eventoCriado = await _eventoService.PostEvento(eventoRequestDTO);

            if (eventoCriado == null)
                return BadRequest(new { mensagem = "Erro ao criar o evento." });

            return CreatedAtAction(nameof(GetEvento), new { id = eventoCriado.Id }, eventoCriado);
        }

        // Atualizar evento
        [HttpPut("evento/{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] EventoDTORequest eventoRequestDTO)
        {

            try
            {
                var eventoAtualizado = await _eventoService.PutEvento(id, eventoRequestDTO);
                if (eventoAtualizado == null)
                    return NotFound(new { mensagem = "Evento não encontrado para atualização." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // Apagar evento
        [HttpDelete("evento/{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            return await _eventoService.DeleteEvento(id);
        }

    }
}
