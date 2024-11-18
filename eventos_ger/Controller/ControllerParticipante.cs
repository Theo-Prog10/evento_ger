using eventos_ger.Model.DTOs;
using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Controller;

[ApiController]
public class ControllerParticipantes : ControllerBase
{
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IEventoRepository  _eventoRepository;

    public ControllerParticipantes(IParticipanteRepository participanteRepository)
    {
        _participanteRepository = participanteRepository;
    }

    // Lista todos os participantes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParticipanteDTO>>> GetParticipantes()
    {
        return Ok(await _participanteRepository.ObterTodosAsync());
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
            EventosInscritos = participante.Eventos_inscritos
        };

        return Ok(participanteDTO);
    }

    // Cria um novo participante
    [HttpPost]
    public async Task<ActionResult> PostParticipante(ParticipanteDTO participanteDTO)
    {
        // Aqui você converte a lista de nomes para lista de IDs de eventos
        var eventoIds = new List<int>();

        foreach (var eventoNome in participanteDTO.EventosInscritos)
        {
            // busca o ID do evento pelo nome através do repositório
            var evento = await _eventoRepository.ObterPorNomeAsync(eventoNome);

            if (evento != null)
            {
                eventoIds.Add(evento.Id); // Adiciona o ID do evento à lista
            }
        }

        // Convertendo DTO para entidade
        var participante = new Participante
        {
            nome = participanteDTO.Nome,
            nascimento = participanteDTO.Nascimento,
            Eventos_inscritos = eventoIds  // Atribuindo a lista de IDs dos eventos
        };

        await _participanteRepository.AdicionarAsync(participante);

        return Ok(new { mensagem = "Participante criado com sucesso." });
    }


    // Atualiza informações de um participante
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipante(int id, ParticipanteDTO participanteDTO)
    {
        // Convertendo DTO para entidade
        var participante = new Participante
        {
            nome = participanteDTO.Nome,
            nascimento = participanteDTO.Nascimento,
            Eventos_inscritos = participanteDTO.EventosInscritos
        };

        await _participanteRepository.AtualizarAsync(participante);
        return NoContent();
    }

    // Deleta um participante
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        await _participanteRepository.DeletarAsync(id);
        return NoContent();
    }
}
