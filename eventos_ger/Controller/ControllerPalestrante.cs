using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Repository.Interfaces;

[ApiController]
public class ControllerPalestrante : ControllerBase
{
    private readonly IPalestranteRepository _palestranteRepository;
    private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

    public ControllerPalestrante(IPalestranteRepository palestranteRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
    {
        _palestranteRepository = palestranteRepository;
        _associacaoEventoPessoa = associacaoEventoPessoa;
    }

    // Lista todos os palestrantes
    [HttpGet("palestrantes")]
    public async Task<ActionResult<IEnumerable<Palestrante>>> GetPalestrantes()
    {
        var palestrantes = await _palestranteRepository.ObterTodosAsync();
        var palestrantesDTO = palestrantes.Select(palestrante => new PalestranteDTO
        {
            Id = palestrante.Id,
            Nome = palestrante.nome,
            Biografia = palestrante.biografia,
            Especialidade = palestrante.especialidade,
            Cpf = palestrante.cpf,
            Nascimento = palestrante.nascimento
        }).ToList();
        
        foreach (PalestranteDTO palestrante in palestrantesDTO)
        {
            palestrante.PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestrante.Id, "Palestrante");
        }

        return Ok(palestrantesDTO);
    }

    // Busca palestrante por ID
    [HttpGet("palestrante/{id}")]
    public async Task<ActionResult<PalestranteDTO>> GetPalestrante(int id)
    {
        var palestrante = await _palestranteRepository.ObterPorIdAsync(id);
    
        if (palestrante == null) 
            return NotFound(new { mensagem = "Palestrante não encontrado." });
        
        // Convertendo Palestrante para PalestranteDTO sem preocupação com os eventos
        var palestranteDTO = new PalestranteDTO
        {
            Nome = palestrante.nome,
            Biografia = palestrante.biografia,
            Especialidade = palestrante.especialidade,
            Cpf = palestrante.cpf,
            Nascimento = palestrante.nascimento
        };
        
        palestranteDTO.PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestranteDTO.Id, "Palestrante");

        return Ok(palestranteDTO);
    }

    // Cria um novo palestrante
    [HttpPost("/palestrantes")]
    public async Task<ActionResult<PalestranteDTO>> PostPalestrante(PalestranteDTO palestranteDTO)
    {
        // Convertendo PalestranteDTO para entidade Palestrante
        var palestrante = new Palestrante
        {
            nome = palestranteDTO.Nome,
            biografia = palestranteDTO.Biografia,
            especialidade = palestranteDTO.Especialidade,
            cpf = palestranteDTO.Cpf,
            nascimento = palestranteDTO.Nascimento
            
        };
        
        await _palestranteRepository.AdicionarAsync(palestrante);

        return Ok("Palestrante adicionado com sucesso.");
    }

    // Atualiza as informações de um palestrante
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

    // Deleta um palestrante
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePalestrante(int id)
    {
        if (!await _palestranteRepository.ExisteAsync(id)) return NotFound();

        await _palestranteRepository.DeletarAsync(id);
        return NoContent();
    }
}
