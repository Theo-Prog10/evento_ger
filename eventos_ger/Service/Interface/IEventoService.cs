using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Service.Interface;

public interface IEventoService
{
    Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventos();
    Task<ActionResult<EventoDTOResponse>> GetEvento(int id);
    Task<EventoDTOResponse> PostEvento(EventoDTORequest eventoDTORequest);
    Task<IActionResult> PutEvento(int id, EventoDTORequest eventoDTORequest);
    Task<IActionResult> DeleteEvento(int id);
}