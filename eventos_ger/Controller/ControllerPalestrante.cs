using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Repository.Interfaces;

[ApiController]
public class ControllerPalestrante : ControllerBase
{
    private readonly IPalestranteRepository _palestranteRepository;

    public ControllerPalestrante(IPalestranteRepository palestranteRepository)
    {
        _palestranteRepository = palestranteRepository;
    }

    [HttpGet("api/palestrantes")]
    public async Task<ActionResult<IEnumerable<PalestranteDTO>>> GetPalestrantes()
    {
        return Ok(await _palestranteRepository.ObterTodosAsync());
    }

    [HttpGet("api/palestrantes/{id}")]
    public async Task<ActionResult<PalestranteDTO>> GetPalestrante(int id)
    {
        var palestrante = await _palestranteRepository.ObterPorIdAsync(id);
        
        if (palestrante == null) return 
            NotFound(new { mensagem = "Palestrante não encontrado." });

        // Convertendo Palestrante para PalestranteDTO
        var palestranteDTO = new PalestranteDTO
        {
            Nome = palestrante.nome,
            Biografia = palestrante.biografia,
            Especialidade = palestrante.especialidade
        };

        return Ok(palestranteDTO);
    }

    [HttpPost("/palestrantes")]
    public async Task<ActionResult<PalestranteDTO>> PostPalestrante(PalestranteDTO palestranteDTO)
    {
        // Convertendo PalestranteDTO para entidade Palestrante
        var palestrante = new Palestrante
        {
            nome = palestranteDTO.Nome,
            biografia = palestranteDTO.Biografia,
            especialidade = palestranteDTO.Especialidade
        };
        
        var novoPalestrante = await _palestranteRepository.AdicionarAsync(palestrante);
        

        return Ok("adicionado");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPalestrante(int id, PalestranteDTO inputPalestranteDTO)
    {
        if (!await _palestranteRepository.ExisteAsync(id)) return NotFound();

        // Convertendo DTO para entidade
        var palestrante = new Palestrante
        {
            Id = id,
            nome = inputPalestranteDTO.Nome,
            biografia = inputPalestranteDTO.Biografia,
            especialidade = inputPalestranteDTO.Especialidade
        };

        await _palestranteRepository.AtualizarAsync(palestrante);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePalestrante(int id)
    {
        if (!await _palestranteRepository.ExisteAsync(id)) return NotFound();

        await _palestranteRepository.DeletarAsync(id);
        return NoContent();
    }
}
