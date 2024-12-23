using eventos_ger.Model.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Service.Interface;

public interface IEventoService
{
    Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos();
    Task<ActionResult<EventoDTO>> GetEvento(int id);
    Task<ActionResult<EventoDTO>> PostEvento(EventoDTO eventoDTO);
    Task<IActionResult> PutEvento(int id, EventoDTO eventoDTO);
    Task<IActionResult> DeleteEvento(int id);
}