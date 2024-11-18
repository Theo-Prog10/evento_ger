using eventos_ger.Model.DTOs;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller;

[ApiController]
public class ControllerParticipantes : ControllerBase
{
    private readonly IParticipanteRepository _participanteRepository;
    

    public ControllerParticipantes(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository)
    {
        _participanteRepository = participanteRepository;
    }

    // Lista todos os participantes
    [HttpGet("participante")]
    public async Task<ActionResult<IEnumerable<ParticipanteDTO>>> GetParticipantes()
    {
        var participantes = await _participanteRepository.ObterTodosAsync();

        var participantesDTO = participantes.Select(participante => new ParticipanteDTO
        {
            Id = participante.Id,
            Nome = participante.nome,
            nascimento = participante.nascimento,
            cpf = participante.cpf,
            tipo_ingresso = participante.tipo_ingresso,
            status_inscricao = participante.status_inscricao,
            // Lista de eventos inscritos vazia, sem busca
            EventosInscritos = new List<int>()
        }).ToList();

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
            nascimento = participante.nascimento,
            tipo_ingresso = participante.tipo_ingresso,
            status_inscricao = participante.status_inscricao,
            // Lista de eventos inscritos vazia, sem busca
            EventosInscritos = new List<int>()
        };

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
            nascimento = participanteDTO.nascimento,
            tipo_ingresso = participanteDTO.tipo_ingresso,
            status_inscricao = participanteDTO.status_inscricao,
            cpf = participanteDTO.cpf,
            Eventos_inscritos = new List<int>()  // Lista de eventos vazia
        };

        await _participanteRepository.AdicionarAsync(participante);

        return Ok(new { mensagem = "Participante criado com sucesso." });
    }

    // Atualiza informações de um participante
    [HttpPut("participante/{id}")]
    public async Task<IActionResult> PutParticipante(int id, ParticipanteDTO participanteDTO)
    {
        // Convertendo DTO para entidade
        var participante = new Participante
        {
            nome = participanteDTO.Nome,
            nascimento = participanteDTO.nascimento,
            Eventos_inscritos = new List<int>()  // Lista de eventos vazia
        };

        await _participanteRepository.AtualizarAsync(participante);
        return NoContent();
    }

    // Deleta um participante
    [HttpDelete("participante/{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        await _participanteRepository.DeletarAsync(id);
        return NoContent();
    }
}
