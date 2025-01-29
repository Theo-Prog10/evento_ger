using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Model.DTOs.Response; // Adicione a referência à sua classe de resposta
using eventos_ger.Model.DTOs.Request;
using System;
using System.Threading.Tasks;
using eventos_ger.Service.Interface;

namespace eventos_ger.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;
        private readonly IUsuarioRepository _usuarioRepository;

        public PessoaService(IPessoaRepository pessoaRepository, IAssociacaoEventoPessoa associacaoEventoPessoa, IUsuarioRepository usuarioRepository)
        {
            _pessoaRepository = pessoaRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResponse> ValidarLoginAsync(string login, string senha)
        {
            // Verifica se existe uma pessoa com o login e senha informados
            var pessoa = await _usuarioRepository.ObterPorLoginESenhaAsync(login, senha);

            if (pessoa == null)
            {
                // Lança exceção se não encontrar a pessoa
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }
            

            return new LoginResponse
            {
                Login = login
            };
        }
        
        public async Task<IEnumerable<PessoaDTOResponse>> ObterTodosAsync()
        {
            var pessoas = await _pessoaRepository.ObterTodosAsync();
    
            var pessoasDTO = pessoas.Select(p => new PessoaDTOResponse
            {
                Nome = p.nome,
                Nascimento = p.nascimento,
                Cpf = p.cpf
            }).ToList();

            // Agora, você usa o 'id' diretamente da entidade Participante
            foreach (var pessoa in pessoas)
            {
                // Aqui você pode acessar o 'id' diretamente
                var eventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Participante");
                var eventosPalestrados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Palestrante");
                var eventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(pessoa.Id, "Organizador");

                // Você agora preenche o DTO com os eventos, sem expor o 'id' diretamente
                var dtoPessoa = pessoasDTO.FirstOrDefault(p => p.Nome == pessoa.nome); // Assumindo que o nome é único
                if (dtoPessoa != null)
                {
                    dtoPessoa.EventosInscritos = eventosInscritos;
                    dtoPessoa.EventosPalestrados = eventosPalestrados;
                    dtoPessoa.EventosOrganizados = eventosOrganizados;
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
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
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
                Nome = pessoaCriada.nome,
                Nascimento = pessoaCriada.nascimento,
                Cpf = pessoaCriada.cpf,
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

            await _pessoaRepository.AtualizarAsync(pessoa);

            // Retornar o PessoaDTOResponse após atualização
            return new PessoaDTOResponse
            {
                Nome = pessoa.nome,
                Nascimento = pessoa.nascimento,
                Cpf = pessoa.cpf,
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
    }
}