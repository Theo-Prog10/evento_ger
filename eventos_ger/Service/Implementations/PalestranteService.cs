using eventos_ger.Model;
using eventos_ger.Model.DTOs;
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

        public async Task<IEnumerable<PalestranteDTO>> ObterTodosAsync()
        {
            var palestrantes = await _palestranteRepository.ObterTodosAsync();
            var palestrantesDTO = palestrantes.Select(p => new PalestranteDTO
            {
                Id = p.Id,
                Nome = p.nome,
                Biografia = p.biografia,
                Especialidade = p.especialidade,
                Cpf = p.cpf,
                Nascimento = p.nascimento
            }).ToList();

            foreach (var palestrante in palestrantesDTO)
            {
                palestrante.PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestrante.Id, "Palestrante");
            }

            return palestrantesDTO;
        }

        public async Task<PalestranteDTO?> ObterPorIdAsync(int id)
        {
            var palestrante = await _palestranteRepository.ObterPorIdAsync(id);
            if (palestrante == null) return null;

            return new PalestranteDTO
            {
                Id = palestrante.Id,
                Nome = palestrante.nome,
                Biografia = palestrante.biografia,
                Especialidade = palestrante.especialidade,
                Cpf = palestrante.cpf,
                Nascimento = palestrante.nascimento,
                PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(palestrante.Id, "Palestrante")
            };
        }

        public async Task<PalestranteDTO> CriarAsync(PalestranteDTO palestranteDTO)
        {
            var palestrante = new Palestrante
            {
                nome = palestranteDTO.Nome,
                biografia = palestranteDTO.Biografia,
                especialidade = palestranteDTO.Especialidade,
                cpf = palestranteDTO.Cpf,
                nascimento = palestranteDTO.Nascimento
            };

            var criado = await _palestranteRepository.AdicionarAsync(palestrante);

            return new PalestranteDTO
            {
                Id = criado.Id,
                Nome = criado.nome,
                Biografia = criado.biografia,
                Especialidade = criado.especialidade,
                Cpf = criado.cpf,
                Nascimento = criado.nascimento,
                PalestrasMinistradas = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Palestrante")
            };
        }

        public async Task AtualizarAsync(int id, PalestranteDTO palestranteDTO)
        {
            var palestranteExistente = await _palestranteRepository.ObterPorIdAsync(id);

            if (palestranteExistente == null)
                throw new ArgumentException("Palestrante não encontrado.");

            palestranteExistente.nome = palestranteDTO.Nome;
            palestranteExistente.biografia = palestranteDTO.Biografia;
            palestranteExistente.especialidade = palestranteDTO.Especialidade;
            palestranteExistente.cpf = palestranteDTO.Cpf;
            palestranteExistente.nascimento = palestranteDTO.Nascimento;

            await _palestranteRepository.AtualizarAsync(palestranteExistente);
        }

        public async Task RemoverAsync(int id)
        {
            if (!await _palestranteRepository.ExisteAsync(id))
                throw new ArgumentException("Palestrante não encontrado.");

            await _palestranteRepository.DeletarAsync(id);
        }
    }
}
