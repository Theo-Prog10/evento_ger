﻿using eventos_ger.Model;
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
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public EventoService(IEventoRepository eventoRepository, IPessoaRepository pessoaRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _eventoRepository = eventoRepository;
            _pessoaRepository = pessoaRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

        public async Task<ActionResult<IEnumerable<EventoDTOResponse>>> GetEventosPessoa(string login, string tipo_pessoa)
        {
            // Obter o ID da pessoa pelo login
            var pessoa = await _pessoaRepository.ObterPorLoginAsync(login);
            if (pessoa == null)
            {
                return new NotFoundObjectResult("Usuário não encontrado.");
            }

            // Buscar eventos associados ao tipo de pessoa (participante, palestrante ou organizador)
            var eventosIds = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, tipo_pessoa);

            var eventosDTO = new List<EventoDTOResponse>();

            // Loop para obter detalhes de cada evento pelo ID
            foreach (var eventoId in eventosIds)
            {
                var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
                if (evento != null)
                {
                    eventosDTO.Add(new EventoDTOResponse
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
                    });
                }
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
            var organizador = await _pessoaRepository.ObterPorIdAsync(eventoDTORequest.IdOrganizador);
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