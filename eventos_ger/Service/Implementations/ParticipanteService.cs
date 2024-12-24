using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;

namespace eventos_ger.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;

        public ParticipanteService(IParticipanteRepository participanteRepository, IAssociacaoEventoPessoa associacaoEventoPessoa)
        {
            _participanteRepository = participanteRepository;
            _associacaoEventoPessoa = associacaoEventoPessoa;
        }

        public async Task<IEnumerable<ParticipanteDTO>> ObterTodosAsync()
        {
            var participantes = await _participanteRepository.ObterTodosAsync();
            var participantesDTO = participantes.Select(p => new ParticipanteDTO
            {
                Id = p.Id,
                Nome = p.nome,
                Nascimento = p.nascimento,
                Cpf = p.cpf,
                Status_inscricao = p.status_inscricao,
                Tipo_ingresso = p.tipo_ingresso
            }).ToList();
            
            foreach (var participante in participantesDTO)
            {
                participante.EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante");
            }

            return participantesDTO;
        }

        public async Task<ParticipanteDTO> ObterPorIdAsync(int id)
        {
            var participante = await _participanteRepository.ObterPorIdAsync(id);
            if (participante == null) return null;

            return new ParticipanteDTO
            {
                Id = participante.Id,
                Nome = participante.nome,
                Nascimento = participante.nascimento,
                Cpf = participante.cpf,
                Status_inscricao = participante.status_inscricao,
                Tipo_ingresso = participante.tipo_ingresso,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante")
            };
        }

        public async Task<ParticipanteDTO> CriarAsync(ParticipanteDTO participanteDTO)
        {
            var participante = new Participante
            {
                nome = participanteDTO.Nome,
                nascimento = participanteDTO.Nascimento,
                cpf = participanteDTO.Cpf,
                status_inscricao = participanteDTO.Status_inscricao,
                tipo_ingresso = participanteDTO.Tipo_ingresso
            };

            var criado = await _participanteRepository.AdicionarAsync(participante);

            return new ParticipanteDTO
            {
                Id = criado.Id,
                Nome = criado.nome,
                Nascimento = criado.nascimento,
                Cpf = criado.cpf,
                Status_inscricao = criado.status_inscricao,
                Tipo_ingresso = criado.tipo_ingresso,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Participante")
            };
        }

        public async Task AtualizarAsync(int id, ParticipanteDTO participanteDTO)
        {
            var participante = await _participanteRepository.ObterPorIdAsync(id);

            if (participante == null)
            {
                throw new ArgumentException("Participante não encontrado.");
            }

            participante.nome = participanteDTO.Nome;
            participante.nascimento = participanteDTO.Nascimento;
            participante.cpf = participanteDTO.Cpf;
            participante.status_inscricao = participanteDTO.Status_inscricao;
            participante.tipo_ingresso = participanteDTO.Tipo_ingresso;

            await _participanteRepository.AtualizarAsync(participante);
        }

        public async Task RemoverAsync(int id)
        {
            if (!await _participanteRepository.ExisteAsync(id))
            {
                throw new ArgumentException("Participante não encontrado.");
            }

            await _participanteRepository.DeletarAsync(id);
        }
    }
}
