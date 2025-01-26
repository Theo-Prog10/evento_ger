using eventos_ger.Model;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Model.DTOs.Request;
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

        public async Task<IEnumerable<OrganizadorDTOResponse>> ObterTodosAsync()
        {
            var organizadores = await _organizadorRepository.ObterTodosAsync();
    
            var organizadoresDTO = organizadores.Select(o => new OrganizadorDTOResponse
            {
                Nome = o.nome,
                Nascimento = o.nascimento,
                Cpf = o.cpf,
                Contato = o.contato
            }).ToList();

            // Agora, você usa o 'id' diretamente da entidade Organizador
            foreach (var organizador in organizadores)
            {
                // Aqui você pode acessar o 'id' diretamente
                var eventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(organizador.Id, "Organizador");
        
                // Você agora preenche o DTO com os eventos organizados, sem expor o 'id' diretamente
                var dtoOrganizador = organizadoresDTO.FirstOrDefault(o => o.Nome == organizador.nome); // Assumindo que o nome é único
                if (dtoOrganizador != null)
                {
                    dtoOrganizador.EventosOrganizados = eventosOrganizados;
                }
            }

            return organizadoresDTO;
        }


        public async Task<OrganizadorDTOResponse?> ObterPorIdAsync(int id)
        {
            var organizador = await _organizadorRepository.ObterPorIdAsync(id);
            if (organizador == null) return null;

            return new OrganizadorDTOResponse
            {
                Nome = organizador.nome,
                Nascimento = organizador.nascimento,
                Cpf = organizador.cpf,
                Contato = organizador.contato,
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(organizador.Id, "Organizador")
            };
        }

        public async Task<OrganizadorDTOResponse> CriarAsync(OrganizadorDTORequest organizadorDTORequest)
        {
            var organizador = new Organizador
            {
                nome = organizadorDTORequest.Nome,
                nascimento = organizadorDTORequest.Nascimento,
                cpf = organizadorDTORequest.Cpf,
                contato = organizadorDTORequest.Contato,
                Login = organizadorDTORequest.Login,
                Senha = organizadorDTORequest.Senha
            };

            var criado = await _organizadorRepository.AdicionarAsync(organizador);

            return new OrganizadorDTOResponse
            {
                Nome = criado.nome,
                Nascimento = criado.nascimento,
                Cpf = criado.cpf,
                Contato = criado.contato,
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Organizador")
            };
        }

        public async Task<OrganizadorDTOResponse> AtualizarAsync(int id, OrganizadorDTORequest organizadorDTORequest)
        {
            var organizadorExistente = await _organizadorRepository.ObterPorIdAsync(id);

            if (organizadorExistente == null)
                throw new ArgumentException("Organizador não encontrado.");

            organizadorExistente.nome = organizadorDTORequest.Nome;
            organizadorExistente.contato = organizadorDTORequest.Contato;
            organizadorExistente.cpf = organizadorDTORequest.Cpf;
            organizadorExistente.nascimento = organizadorDTORequest.Nascimento;

            await _organizadorRepository.AtualizarAsync(organizadorExistente);

            return new OrganizadorDTOResponse
            {
                Nome = organizadorExistente.nome,
                Nascimento = organizadorExistente.nascimento,
                Cpf = organizadorExistente.cpf,
                Contato = organizadorExistente.contato,
                EventosOrganizados = await _associacaoEventoPessoa.ObterEventosAsync(organizadorExistente.Id, "Organizador")
            };
        }

        public async Task RemoverAsync(int id)
        {
            if (!await _organizadorRepository.ExisteAsync(id))
                throw new ArgumentException("Organizador não encontrado.");

            await _organizadorRepository.DeletarAsync(id);
        }
    }
}
