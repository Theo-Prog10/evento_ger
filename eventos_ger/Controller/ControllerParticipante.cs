using eventos_ger.Model.DTOs;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller;

[ApiController]
public class ControllerParticipantes : ControllerBase
{
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;
    
    public ControllerParticipantes(IParticipanteRepository participanteRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
    {
        _participanteRepository = participanteRepository;
        _associacaoEventoPessoa = associacaoEventoPessoa;
    }

    // Lista todos os participantes
    [HttpGet("participantes")]
    public async Task<ActionResult<IEnumerable<ParticipanteDTO>>> GetParticipantes()
    {
        var participantes = await _participanteRepository.ObterTodosAsync();

        var participantesDTO = participantes.Select(participante => new ParticipanteDTO
        {
            Id = participante.Id,
            Nome = participante.nome,
            Nascimento = participante.nascimento,
            Cpf = participante.cpf,
            Tipo_ingresso = participante.tipo_ingresso,
            Status_inscricao = participante.status_inscricao
        }).ToList();
        
        foreach (ParticipanteDTO participante in participantesDTO)
        {
            participante.EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante");
        }

        return Ok(participantesDTO);
    }

    // Busca participante por ID
    [HttpGet("participante/{id}")]
    public async Task<ActionResult<ParticipanteDTO>> GetParticipante(int id)
    {
        var participante = await _participanteRepository.ObterPorIdAsync(id);

        if (participante == null)
        {
            return NotFound(new { mensagem = "Participante não encontrado." });
        }

        // Convertendo a entidade para DTO
        var participanteDTO = new ParticipanteDTO
        {
            Id = participante.Id,
            Nome = participante.nome,
            Nascimento = participante.nascimento,
            Tipo_ingresso = participante.tipo_ingresso,
            Status_inscricao = participante.status_inscricao
        };
        
        participanteDTO.EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participanteDTO.Id, "Participante");

        return Ok(participanteDTO);
    }

    // Cria um novo participante
    [HttpPost("participante")]
    public async Task<ActionResult> PostParticipante(ParticipanteDTO participanteDTO)
    {
        // Convertendo DTO para entidade
        var participante = new Participante
        {
            nome = participanteDTO.Nome,
            nascimento = participanteDTO.Nascimento,
            tipo_ingresso = participanteDTO.Tipo_ingresso,
            status_inscricao = participanteDTO.Status_inscricao,
            cpf = participanteDTO.Cpf
            
        };

        await _participanteRepository.AdicionarAsync(participante);

        return Ok(new { mensagem = "Participante criado com sucesso." });
    }

    // Atualiza informações de um participante
    [HttpPut("participante/{id}")]
    public async Task<IActionResult> AtualizarParticipante(int id, ParticipanteDTO participanteDto)
    {
        // Verifica se o participante existe
        var participanteExistente = await _participanteRepository.ObterPorIdAsync(id);
        if (participanteExistente == null)
        {
            return NotFound(new { mensagem = "Participante não encontrado." });
        }

        // Mapeia os dados do DTO para a entidade Participante
        participanteExistente.nome = participanteDto.Nome;
        participanteExistente.nascimento = participanteDto.Nascimento;
        participanteExistente.cpf = participanteDto.Cpf;
        participanteExistente.tipo_ingresso = participanteDto.Tipo_ingresso;
        participanteExistente.status_inscricao = participanteDto.Status_inscricao;

        // Atualiza o participante (mas não mexe na lista de eventos)
        try
        {
            await _participanteRepository.AtualizarAsync(participanteExistente);
            return NoContent();  // Retorna NoContent após a atualização bem-sucedida
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno", detalhe = ex.Message });
        }
    }


    // Deleta um participante
    [HttpDelete("participante/{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        if (!await _participanteRepository.ExisteAsync(id)) return NotFound();
        await _participanteRepository.DeletarAsync(id);
        return NoContent();
    }
}
