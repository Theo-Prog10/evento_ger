using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    // Obter todos as pessoas (retorna PessoaDTOResponse)
    [HttpGet("pessoas")]
    public async Task<ActionResult<IEnumerable<PessoaDTOResponse>>> GetPessoas()
    {
        var pessoas = await _pessoaService.ObterTodosAsync();
        return Ok(pessoas);
    }

    // Obter pessoa por ID (retorna PessoaDTOResponse)
    [HttpGet("pessoa/{id}")]
    public async Task<ActionResult<PessoaDTOResponse>> GetPessoas(int id)
    {
        var pessoa = await _pessoaService.ObterPorIdAsync(id);
        if (pessoa == null) return NotFound();
        return (pessoa);
    }
    
    // Obter pessoa por evento (retorna PessoaDTOResponse)
    [HttpGet("evento/{idEvento}/{tipo_pessoa}")]
    public async Task<ActionResult<IEnumerable<PessoaDTOResponse>>> GetPessoasEvento(int idEvento, string tipo_pessoa)
    {
        var pessoas = await _pessoaService.ObterPessoasEvento(idEvento, tipo_pessoa);
        if (pessoas == null) return NotFound();
        return Ok(pessoas);
    }
    
    // Obter pessoa por Login (retorna PessoaDTOResponse)
    [HttpGet("pessoa/{login}")]
    public async Task<ActionResult<PessoaDTOResponse>> GetPessoas(string login)
    {
        var pessoa = await _pessoaService.ObterPorLoginAsync(login);
        if (pessoa == null) return NotFound();
        return (pessoa);
    }
    

    // Criar pessoa (usa PessoaDTORequest para a entrada e retorna PessoaDTOResponse)
    [HttpPost("pessoa")]
    public async Task<ActionResult<PessoaDTOResponse>> PostPessoa(PessoaDTORequest pessoaDTO)
    {
        var criado = await _pessoaService.CriarAsync(pessoaDTO);
        return CreatedAtAction(nameof(GetPessoas), new { nome = criado.Nome }, criado);
    }

    // Atualizar pessoa (usa PessoaDTORequest para a entrada)
    [HttpPut("pessoa/{id}")]
    public async Task<IActionResult> PutPessoa(int id, PessoaDTORequest pessoaDTO)
    {
        if (id != pessoaDTO.Id) return BadRequest();

        try
        {
            await _pessoaService.AtualizarAsync(id, pessoaDTO);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

    // Remover pessoa (sem necessidade de DTO, já que o ID é suficiente)
    [HttpDelete("pessoa/{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        try
        {
            await _pessoaService.RemoverAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
