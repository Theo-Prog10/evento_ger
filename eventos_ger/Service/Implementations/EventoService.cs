using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Service.Interface;

namespace eventos_ger.Service
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public EventoService(IEventoRepository eventoRepository, IOrganizadorRepository organizadorRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _eventoRepository = eventoRepository;
            _organizadorRepository = organizadorRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

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

            return eventosDTO;
        }

        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);
            if (evento == null)
            {
                return new NotFoundResult();
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

            return eventoDTO;
        }

        public async Task<ActionResult<EventoDTO>> PostEvento(EventoDTO eventoDTO)
        {
            var organizador = await _organizadorRepository.ObterPorIdAsync(eventoDTO.id_organizador);
            if (organizador == null)
            {
                return new NotFoundObjectResult(new { mensagem = "Organizador não encontrado." });
            }

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

            var associacao = new AssociacaoEventoPessoa
            {
                idEvento = evento.Id,
                idPessoa = organizador.Id,
                tipo_pessoa = "Organizador"
            };

            await _associacaoEventoPessoa.AdicionarAsync(associacao);

            return new OkObjectResult("Evento criado com sucesso");
        }

        public async Task<IActionResult> PutEvento(int id, EventoDTO eventoDTO)
        {
            if (id != eventoDTO.Id)
            {
                return new BadRequestObjectResult(new { mensagem = "ID do evento não corresponde ao ID fornecido na URL." });
            }

            var eventoExistente = await _eventoRepository.ObterPorIdAsync(id);
            if (eventoExistente == null)
            {
                return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });
            }

            eventoExistente.nome = eventoDTO.Nome;
            eventoExistente.descricao = eventoDTO.Descricao;
            eventoExistente.data = eventoDTO.Data;
            eventoExistente.horario = eventoDTO.Horario;
            eventoExistente.id_local = eventoDTO.id_local;
            eventoExistente.id_organizador = eventoDTO.id_organizador;

            await _eventoRepository.AtualizarAsync(eventoExistente);

            return new NoContentResult();
        }

        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);
            if (evento == null)
            {
                return new NotFoundResult();
            }

            await _eventoRepository.DeletarAsync(id);

            return new NoContentResult();
        }
    }
}
