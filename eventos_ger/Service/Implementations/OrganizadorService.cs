using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;

namespace eventos_ger.Services
{
    public class OrganizadorService : IOrganizadorService
    {
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public OrganizadorService(IOrganizadorRepository organizadorRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _organizadorRepository = organizadorRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

        public async Task<IEnumerable<OrganizadorDTO>> ObterTodosAsync()
        {
            var organizadores = await _organizadorRepository.ObterTodosAsync();
            var organizadoresDTO = organizadores.Select(o => new OrganizadorDTO
            {
                Id = o.Id,
                Nome = o.nome,
                Nascimento = o.nascimento,
                Cpf = o.cpf,
                Contato = o.contato
            }).ToList();

            foreach (var organizador in organizadoresDTO)
            {
                organizador.EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(organizador.Id, "Organizador");
            }

            return organizadoresDTO;
        }

        public async Task<OrganizadorDTO?> ObterPorIdAsync(int id)
        {
            var organizador = await _organizadorRepository.ObterPorIdAsync(id);
            if (organizador == null) return null;

            return new OrganizadorDTO
            {
                Id = organizador.Id,
                Nome = organizador.nome,
                Nascimento = organizador.nascimento,
                Cpf = organizador.cpf,
                Contato = organizador.contato,
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(organizador.Id, "Organizador")
            };
        }

        public async Task<OrganizadorDTO> CriarAsync(OrganizadorDTO organizadorDTO)
        {
            var organizador = new Organizador
            {
                nome = organizadorDTO.Nome,
                nascimento = organizadorDTO.Nascimento,
                cpf = organizadorDTO.Cpf,
                contato = organizadorDTO.Contato
            };

            var criado = await _organizadorRepository.AdicionarAsync(organizador);

            return new OrganizadorDTO
            {
                Id = criado.Id,
                Nome = criado.nome,
                Nascimento = criado.nascimento,
                Cpf = criado.cpf,
                Contato = criado.contato,
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Organizador")
            };
        }

        public async Task AtualizarAsync(int id, OrganizadorDTO organizadorDTO)
        {
            var organizadorExistente = await _organizadorRepository.ObterPorIdAsync(id);

            if (organizadorExistente == null)
                throw new ArgumentException("Organizador não encontrado.");

            organizadorExistente.nome = organizadorDTO.Nome;
            organizadorExistente.contato = organizadorDTO.Contato;
            organizadorExistente.cpf = organizadorDTO.Cpf;
            organizadorExistente.nascimento = organizadorDTO.Nascimento;

            await _organizadorRepository.AtualizarAsync(organizadorExistente);
        }

        public async Task RemoverAsync(int id)
        {
            if (!await _organizadorRepository.ExisteAsync(id))
                throw new ArgumentException("Organizador não encontrado.");

            await _organizadorRepository.DeletarAsync(id);
        }
    }
}
