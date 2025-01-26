using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class OrganizadorController : ControllerBase
{
    private readonly IOrganizadorService _organizadorService;

    public OrganizadorController(IOrganizadorService organizadorService)
    {
        _organizadorService = organizadorService;
    }

    // Obter todos os organizadores (retorna OrganizadorDTOResponse)
    [HttpGet("organizadores")]
    public async Task<ActionResult<IEnumerable<OrganizadorDTOResponse>>> GetOrganizadores()
    {
        var organizadores = await _organizadorService.ObterTodosAsync();
        return Ok(organizadores);
    }

    // Obter organizador por ID (retorna OrganizadorDTOResponse)
    [HttpGet("organizador/{id}")]
    public async Task<ActionResult<OrganizadorDTOResponse>> GetOrganizador(int id)
    {
        var organizador = await _organizadorService.ObterPorIdAsync(id);
        if (organizador == null) return NotFound(new { mensagem = "Organizador não encontrado." });

        return Ok(organizador);
    }

    // Criar organizador (usa OrganizadorDTORequest para a entrada e retorna OrganizadorDTOResponse)
    [HttpPost("organizadores")]
    public async Task<ActionResult<OrganizadorDTOResponse>> PostOrganizador(OrganizadorDTORequest organizadorDTO)
    {
        var criado = await _organizadorService.CriarAsync(organizadorDTO);
        return CreatedAtAction(nameof(GetOrganizador), new { Nome = criado.Nome }, criado);
    }

    // Atualizar organizador (usa OrganizadorDTORequest para a entrada)
    [HttpPut("organizador/{id}")]
    public async Task<IActionResult> PutOrganizador(int id, OrganizadorDTORequest organizadorDTO)
    {
        try
        {
            await _organizadorService.AtualizarAsync(id, organizadorDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    // Remover organizador (sem necessidade de DTO, já que o ID é suficiente)
    [HttpDelete("organizador/{id}")]
    public async Task<IActionResult> DeleteOrganizador(int id)
    {
        try
        {
            await _organizadorService.RemoverAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
