using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Model.DTOs.Response; // Adicione a referência à sua classe de resposta
using System;
using System.Threading.Tasks;
using eventos_ger.Service.Interface;

namespace eventos_ger.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<LoginResponse> ValidarLoginAsync(string login, string senha)
        {
            // Verifica se existe uma pessoa com o login e senha informados
            var pessoa = await _pessoaRepository.ObterPorLoginESenhaAsync(login, senha);

            if (pessoa == null)
            {
                // Lança exceção se não encontrar a pessoa
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            // Verifica o tipo de pessoa (Participante, Organizador ou Palestrante)
            string tipoPessoa = pessoa switch
            {
                Participante _ => "Participante",
                Organizador _ => "Organizador",
                Palestrante _ => "Palestrante",
                _ => "Desconhecido"
            };

            return new LoginResponse
            {
                TipoPessoa = tipoPessoa
            };
        }
    }
}