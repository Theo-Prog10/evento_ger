using eventos_ger.Service;
using eventos_ger.Model.DTOs;
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

        //listar eventos
        [HttpGet("/eventos")]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos()
        {
            return await _eventoService.GetEventos();
        }

        //listar evento por id
        [HttpGet("evento/{id}")]
        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            return await _eventoService.GetEvento(id);
        }

        //criar evento
        [HttpPost("eventos")]
        public async Task<ActionResult<EventoDTO>> PostEvento(EventoDTO eventoDTO)
        {
            return await _eventoService.PostEvento(eventoDTO);
        }

        //atualizar evento
        [HttpPut("evento/{id}")]
        public async Task<IActionResult> PutEvento(int id, EventoDTO eventoDTO)
        {
            return await _eventoService.PutEvento(id, eventoDTO);
        }

        //apagar evento
        [HttpDelete("evento/{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            return await _eventoService.DeleteEvento(id);
        }
    }
}