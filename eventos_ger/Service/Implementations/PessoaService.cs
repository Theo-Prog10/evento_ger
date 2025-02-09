using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Model.DTOs.Request;
using System;
using System.Threading.Tasks;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eventos_ger.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEventoRepository _eventoRepository;

        public PessoaService(IPessoaRepository pessoaRepository, IAssociacaoEventoPessoa associacaoEventoPessoa, IUsuarioRepository usuarioRepository, IEventoRepository eventoRepository)
        {
            _pessoaRepository = pessoaRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
            _usuarioRepository = usuarioRepository;
            _eventoRepository = eventoRepository;
        }
        
        
        public async Task<IEnumerable<PessoaDTOResponse>> ObterTodosAsync()
        {
            var pessoas = await _pessoaRepository.ObterTodosAsync();
            var pessoasDTO = pessoas.Select(p => new PessoaDTOResponse
            {
                id = p.Id,
                Nome = p.nome,
                Nascimento = p.nascimento,
                Cpf = p.cpf,
                biografia = p.biografia,
                especialidade = p.especialidade,
                contato = p.contato
                
            }).ToList();

            foreach (var pessoa in pessoas)
            {
                var eventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante");
                var eventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante");
                var eventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador");

                var dtoPessoa = pessoasDTO.FirstOrDefault(p => p.Nome == pessoa.nome);
                if (dtoPessoa != null)
                {
                    dtoPessoa.EventosInscritos = eventosInscritos;
                    dtoPessoa.EventosPalestrados = eventosPalestrados;
                    dtoPessoa.EventosOrganizados = eventosOrganizados;
                }
            }

            return pessoasDTO;
        }
        public async Task<IEnumerable<PessoaDTOResponse>> ObterPessoasEvento(int id, string tipo_pessoa)
        {
            // Buscar evento
            var evento = await _eventoRepository.ObterPorIdAsync(id);
            if (evento == null) return null;

            var idsPessoas = await _associacaoEventoPessoa.ObterPessoasAsync(id, tipo_pessoa);
            
            // Mapear para DTO
            var pessoasDTO = new List<PessoaDTOResponse>();

            foreach (var id_p in idsPessoas)
            {
                // Obter detalhes da pessoa pelo ID
                var pessoaDetalhe = await _pessoaRepository.ObterPorIdAsync(id_p);
                if (pessoaDetalhe != null)
                {
                    pessoasDTO.Add(new PessoaDTOResponse
                    {
                        Nome = pessoaDetalhe.nome
                    });
                }
            }

            return pessoasDTO;
        }

        public async Task<PessoaDTOResponse> ObterPorIdAsync(int id)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
            if (pessoa == null) return null;

            return new PessoaDTOResponse
            {
                id = pessoa.Id,
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
                biografia = pessoa.biografia,
                especialidade = pessoa.especialidade,
                contato = pessoa.contato,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante"),
                EventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante"),
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador")
            };
        }
        
        public async Task<PessoaDTOResponse> ObterPorLoginAsync(string login)
        {
            var pessoa = await _pessoaRepository.ObterPorLoginAsync(login);
            if (pessoa == null) return null;

            return new PessoaDTOResponse
            {
                id = pessoa.Id,
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
                biografia = pessoa.biografia,
                especialidade = pessoa.especialidade,
                contato = pessoa.contato,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante"),
                EventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante"),
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador")
            };
        }

        public async Task<PessoaDTOResponse> CriarAsync(PessoaDTORequest pessoaDTORequest)
        {
            // Verifica se o login já existe
            var loginsExistentes = await _usuarioRepository.ObterLoginsAsync();

            if (loginsExistentes.Contains(pessoaDTORequest.login))
            {
                throw new Exception("O login informado já está em uso.");
            }
            
            var pessoa = new Pessoa
            {
                nome = pessoaDTORequest.Nome,
                nascimento = pessoaDTORequest.Nascimento,
                cpf = pessoaDTORequest.Cpf,
                biografia = pessoaDTORequest.biografia,
                contato = pessoaDTORequest.contato,
                especialidade = pessoaDTORequest.especialidade,
            };
            
            var usuario = new Usuario
            {
                login = pessoaDTORequest.login, // Login fornecido no DTO
                senha = pessoaDTORequest.senha // Criptografa a senha
            };


            // Salva o usuário no banco de dados
            var usuarioCriado = await _usuarioRepository.AdicionarAsync(usuario);

            // Associa o ID do usuário à pessoa
            pessoa.id_usuario = usuarioCriado.Id;

            // Salva a pessoa no banco de dados
            var pessoaCriada = await _pessoaRepository.AdicionarAsync(pessoa);

            return new PessoaDTOResponse
            {
                id = pessoa.Id,
                Nome = pessoaCriada.nome,
                Nascimento = pessoaCriada.nascimento,
                Cpf = pessoaCriada.cpf,
                biografia = pessoa.biografia,
                especialidade = pessoa.especialidade,
                contato = pessoa.contato,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoaCriada.Id, "Participante"),
                EventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoaCriada.Id, "Palestrante"),
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoaCriada.Id, "Organizador")
            };
        }


        public async Task<PessoaDTOResponse> AtualizarAsync(int id, PessoaDTORequest pessoaDTORequest)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);

            if (pessoa == null)
            {
                throw new ArgumentException("Pessoa não encontrado.");
            }

            pessoa.nome = pessoaDTORequest.Nome;
            pessoa.nascimento = pessoaDTORequest.Nascimento;
            pessoa.cpf = pessoaDTORequest.Cpf;
            pessoa.biografia = pessoaDTORequest.biografia;
            pessoa.contato = pessoaDTORequest.contato;
            pessoa.especialidade = pessoaDTORequest.especialidade;

            await _pessoaRepository.AtualizarAsync(pessoa);

            // Retornar o PessoaDTOResponse após atualização
            return new PessoaDTOResponse
            {
                id = pessoa.Id,
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
                biografia = pessoa.biografia,
                especialidade = pessoa.especialidade,
                contato = pessoa.contato,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante"),
                EventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante"),
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador")
            };
        }


        public async Task RemoverAsync(int id)
        {
            if (!await _pessoaRepository.ExisteAsync(id))
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            await _pessoaRepository.DeletarAsync(id);
        }
        
        public async Task<PessoaDTOResponse?> ValidarLoginAsync(string login, string senha)
        {
            var pessoa = await _pessoaRepository.ObterPorLoginSenhaAsync(login, senha);

            if (pessoa == null)
                return null;

            return new PessoaDTOResponse
            {
                id = pessoa.Id,
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
                biografia = pessoa.biografia,
                especialidade = pessoa.especialidade,
                contato = pessoa.contato,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante"),
                EventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante"),
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador")
            };
        }
    }
}