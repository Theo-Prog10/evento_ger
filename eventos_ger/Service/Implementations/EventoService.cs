using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
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

        public async Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventos()
        {
            var eventos = await _eventoRepository.ObterEventosAsync();

            // Converter eventos para lista para permitir acesso por índice
            var eventosList = eventos.ToList();

            // Criar uma lista para armazenar os eventos DTOs
            var eventosDTO = new List<EventoDTOResponse>();

            // Criar as tarefas para obter os palestrantes e participantes
            var palestrantesTasks = eventosList.Select(e => _associacaoEventoPessoa.ObterPessoasAsync(e.Id, "Palestrante")).ToList();
            var participantesTasks = eventosList.Select(e => _associacaoEventoPessoa.ObterPessoasAsync(e.Id, "Participante")).ToList();

            // Esperar por todas as tarefas de palestrantes e participantes
            var palestrantes = await Task.WhenAll(palestrantesTasks);
            var participantes = await Task.WhenAll(participantesTasks);

            // Preencher os eventosDTO com as informações
            for (int i = 0; i < eventosList.Count; i++)
            {
                eventosDTO.Add(new EventoDTOResponse
                {
                    Id = eventosList[i].Id,
                    Nome = eventosList[i].nome,
                    Descricao = eventosList[i].descricao,
                    Data = eventosList[i].data,
                    Horario = eventosList[i].horario,
                    IdLocal = eventosList[i].id_local,
                    IdOrganizador = eventosList[i].id_organizador,
                    Palestrantes = palestrantes[i],
                    Participantes = participantes[i]
                });
            }

            return eventosDTO;
        }


        public async Task<ActionResult<EventoDTOResponse>> GetEvento(int id)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(id);
            if (evento == null)
            {
                return new NotFoundResult();
            }

            var eventoDTO = new EventoDTOResponse
            {
                Id = evento.Id,
                Nome = evento.nome,
                Descricao = evento.descricao,
                Data = evento.data,
                Horario = evento.horario,
                IdLocal = evento.id_local,
                IdOrganizador = evento.id_organizador,
                Palestrantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Palestrante"),
                Participantes = await _associacaoEventoPessoa.ObterPessoasAsync(evento.Id, "Participante")
            };

            return eventoDTO;
        }

        public async Task<EventoDTOResponse> PostEvento(EventoDTORequest eventoDTORequest)
        {
            var organizador = await _organizadorRepository.ObterPorIdAsync(eventoDTORequest.IdOrganizador);
            if (organizador == null)
            {
                throw new Exception("Organizador não encontrado."); // Lança uma exceção ou use sua própria lógica de erro
            }

            var evento = new Evento
            {
                nome = eventoDTORequest.Nome,
                descricao = eventoDTORequest.Descricao,
                data = eventoDTORequest.Data,
                horario = eventoDTORequest.Horario,
                id_local = eventoDTORequest.IdLocal,
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

            var eventoDTOResponse = new EventoDTOResponse
            {
                Id = evento.Id,
                Nome = evento.nome,
                Descricao = evento.descricao,
                Data = evento.data,
                Horario = evento.horario,
                IdLocal = evento.id_local,
                IdOrganizador = evento.id_organizador,
                Palestrantes = eventoDTORequest.Palestrantes,
                Participantes = eventoDTORequest.Participantes
            };

            return eventoDTOResponse;
        }


        public async Task<IActionResult> PutEvento(int id, EventoDTORequest eventoDTORequest)
        {

            var eventoExistente = await _eventoRepository.ObterPorIdAsync(id);
            if (eventoExistente == null)
            {
                return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });
            }

            eventoExistente.nome = eventoDTORequest.Nome;
            eventoExistente.descricao = eventoDTORequest.Descricao;
            eventoExistente.data = eventoDTORequest.Data;
            eventoExistente.horario = eventoDTORequest.Horario;
            eventoExistente.id_local = eventoDTORequest.IdLocal;
            eventoExistente.id_organizador = eventoDTORequest.IdOrganizador;

            await _eventoRepository.AtualizarAsync(eventoExistente);

            var eventoDTOResponse = new EventoDTOResponse
            {
                Id = eventoExistente.Id,
                Nome = eventoExistente.nome,
                Descricao = eventoExistente.descricao,
                Data = eventoExistente.data,
                Horario = eventoExistente.horario,
                IdLocal = eventoExistente.id_local,
                IdOrganizador = eventoExistente.id_organizador,
                Palestrantes = eventoDTORequest.Palestrantes,
                Participantes = eventoDTORequest.Participantes
            };

            return new OkObjectResult(eventoDTOResponse);
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
