using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Service.Interface;

public interface IEventoService
{
    // Obter todos os eventos
    Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventos();

    // Obter evento por ID
    Task<ActionResult<EventoDTOResponse>> GetEvento(int id);

    // Criar evento
    Task<EventoDTOResponse> PostEvento(EventoDTORequest eventoDTORequest);

    // Atualizar evento
    Task<IActionResult> PutEvento(int id, EventoDTORequest eventoDTORequest);

    // Deletar evento
    Task<IActionResult> DeleteEvento(int id);
}