using eventos_ger.Model;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Controller
{
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public EventoController(IEventoRepository eventoRepository, IOrganizadorRepository organizadorRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _eventoRepository = eventoRepository;
            _organizadorRepository = organizadorRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

        // listar eventos
        [HttpGet("/eventos")]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos()
        {
            var eventos = await _eventoRepository.ObterEventosAsync();

            var eventosDTO = eventos.Select(e => new EventoDTO
            {
                Id = e.Id,
                Nome = e.nome,
                Descricao = e.descricao,
                Data = e.data,
                Horario = e.horario,
                id_local = e.id_local,
                id_organizador = e.id_organizador
            }).ToList();
            
            foreach (EventoDTO evento in eventosDTO)
            {
                evento.Participantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Participante");
                evento.Palestrantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Palestrante");
            }

            return Ok(eventosDTO);
        }

        // listar evento por id
        [HttpGet("evento/{id}")]
        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            var eventoDTO = new EventoDTO
            {
                Id = evento.Id,
                Nome = evento.nome,
                Descricao = evento.descricao,
                Data = evento.data,
                Horario = evento.horario,
                id_local = evento.id_local,
                id_organizador = evento.id_organizador
            };
            
            eventoDTO.Participantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Participante");
            eventoDTO.Palestrantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Palestrante");
            

            return Ok(eventoDTO);
        }

        // criar evento
        [HttpPost("eventos")]
        public async Task<ActionResult<EventoDTO>> PostEvento(EventoDTO eventoDTO)
        {
            // Verifica se o organizador existe
            var organizador = await _organizadorRepository.ObterPorIdAsync(eventoDTO.id_organizador);

            if (organizador == null)
            {
                return NotFound(new { mensagem = "Organizador não encontrado." });
            }

            // Convertendo DTO para entidade Evento
            var evento = new Evento
            {
                nome = eventoDTO.Nome,
                descricao = eventoDTO.Descricao,
                data = eventoDTO.Data,
                horario = eventoDTO.Horario,
                id_local = eventoDTO.id_local,
                id_organizador = organizador.Id
            };
            
            await _eventoRepository.AdicionarAsync(evento);
            
            AssociacaoEventoPessoa associacao = new AssociacaoEventoPessoa
            {
                idEvento = evento.Id,
                idPessoa = organizador.Id,
                tipo_pessoa = "Organizador"
            };
            
            await _associacaoEventoPessoa.AdicionarAsync(associacao);
            
            return Ok("criado com sucesso");
        }

        // atualizar evento
        [HttpPut("evento/{id}")]
        public async Task<IActionResult> PutEvento(int id, EventoDTO eventoDTO)
        {
            if (id != eventoDTO.Id)
            {
                return BadRequest();
            }

            var eventoExistente = await _eventoRepository.ObterPorIdAsync(id);
            if (eventoExistente == null)
            {
                return NotFound();
            }

            // Verifica se o organizador existe
            var organizador = await _organizadorRepository.ObterPorIdAsync(eventoDTO.id_organizador);
            if (organizador == null)
            {
                return NotFound(new { mensagem = "Organizador não encontrado." });
            }

            // Atualizando os dados do evento
            eventoExistente.nome = eventoDTO.Nome;
            eventoExistente.descricao = eventoDTO.Descricao;
            eventoExistente.data = eventoDTO.Data;
            eventoExistente.horario = eventoDTO.Horario;
            eventoExistente.id_local = eventoDTO.id_local;
            eventoExistente.id_organizador = organizador.Id;

            await _eventoRepository.AtualizarAsync(eventoExistente);

            return NoContent();
        }

        // apagar evento
        [HttpDelete("evento/{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            await _eventoRepository.DeletarAsync(id);
            return NoContent();
        }
    }
}
