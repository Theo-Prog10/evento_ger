using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;

namespace eventos_ger.Services
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public PalestranteService(IPalestranteRepository palestranteRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _palestranteRepository = palestranteRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

        public async Task<IEnumerable<PalestranteDTOResponse>> ObterTodosAsync()
        {
            var palestrantes = await _palestranteRepository.ObterTodosAsync();
    
            var palestrantesDTO = palestrantes.Select(p => new PalestranteDTOResponse
            {
                Nome = p.nome,
                Biografia = p.biografia,
                Especialidade = p.especialidade,
                Cpf = p.cpf,
                Nascimento = p.nascimento
            }).ToList();

            // Agora, você usa o 'id' diretamente da entidade Palestrante
            foreach (var palestrante in palestrantes)
            {
                // Aqui você pode acessar o 'id' diretamente
                var palestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestrante.Id, "Palestrante");
        
                // Você agora preenche o DTO com as palestras, sem expor o 'id' diretamente
                var dtoPalestrante = palestrantesDTO.FirstOrDefault(p => p.Nome == palestrante.nome); // Assumindo que o nome é único
                if (dtoPalestrante != null)
                {
                    dtoPalestrante.PalestrasMinistradas = palestrasMinistradas;
                }
            }

            return palestrantesDTO;
        }


        public async Task<PalestranteDTOResponse?> ObterPorIdAsync(int id)
        {
            var palestrante = await _palestranteRepository.ObterPorIdAsync(id);
            if (palestrante == null) return null;

            return new PalestranteDTOResponse
            {
                Nome = palestrante.nome,
                Biografia = palestrante.biografia,
                Especialidade = palestrante.especialidade,
                Cpf = palestrante.cpf,
                Nascimento = palestrante.nascimento,
                PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestrante.Id, "Palestrante")
            };
        }

        public async Task<PalestranteDTOResponse> CriarAsync(PalestranteDTORequest palestranteDTORequest)
        {
            var palestrante = new Palestrante
            {
                nome = palestranteDTORequest.Nome,
                biografia = palestranteDTORequest.Biografia,
                especialidade = palestranteDTORequest.Especialidade,
                cpf = palestranteDTORequest.Cpf,
                nascimento = palestranteDTORequest.Nascimento,
                Login = palestranteDTORequest.Login,
                Senha = palestranteDTORequest.Senha
            };

            var criado = await _palestranteRepository.AdicionarAsync(palestrante);

            return new PalestranteDTOResponse
            {
                
                Nome = criado.nome,
                Biografia = criado.biografia,
                Especialidade = criado.especialidade,
                Cpf = criado.cpf,
                Nascimento = criado.nascimento,
                PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Palestrante")
            };
        }

        public async Task<PalestranteDTOResponse> AtualizarAsync(int id, PalestranteDTORequest palestranteDTORequest)
        {
            var palestranteExistente = await _palestranteRepository.ObterPorIdAsync(id);

            if (palestranteExistente == null)
                throw new ArgumentException("Palestrante não encontrado.");

            palestranteExistente.nome = palestranteDTORequest.Nome;
            palestranteExistente.biografia = palestranteDTORequest.Biografia;
            palestranteExistente.especialidade = palestranteDTORequest.Especialidade;
            palestranteExistente.cpf = palestranteDTORequest.Cpf;
            palestranteExistente.nascimento = palestranteDTORequest.Nascimento;

            // Atualiza o palestrante no repositório
            await _palestranteRepository.AtualizarAsync(palestranteExistente);

            // Retorna o DTO com os dados atualizados
            return new PalestranteDTOResponse
            {
                Nome = palestranteExistente.nome,
                Biografia = palestranteExistente.biografia,
                Especialidade = palestranteExistente.especialidade,
                Cpf = palestranteExistente.cpf,
                Nascimento = palestranteExistente.nascimento,
                PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestranteExistente.Id, "Palestrante")
            };
        }

        public async Task RemoverAsync(int id)
        {
            if (!await _palestranteRepository.ExisteAsync(id))
                throw new ArgumentException("Palestrante não encontrado.");

            await _palestranteRepository.DeletarAsync(id);
        }
    }
}
