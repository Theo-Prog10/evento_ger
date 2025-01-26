using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;
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

        public async Task<IEnumerable<ParticipanteDTOResponse>> ObterTodosAsync()
        {
            var participantes = await _participanteRepository.ObterTodosAsync();
    
            var participantesDTO = participantes.Select(p => new ParticipanteDTOResponse
            {
                Nome = p.nome,
                Nascimento = p.nascimento,
                Cpf = p.cpf,
                Status_inscricao = p.status_inscricao,
                Tipo_ingresso = p.tipo_ingresso
            }).ToList();

            // Agora, você usa o 'id' diretamente da entidade Participante
            foreach (var participante in participantes)
            {
                // Aqui você pode acessar o 'id' diretamente
                var eventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante");
        
                // Você agora preenche o DTO com os eventos, sem expor o 'id' diretamente
                var dtoParticipante = participantesDTO.FirstOrDefault(p => p.Nome == participante.nome); // Assumindo que o nome é único
                if (dtoParticipante != null)
                {
                    dtoParticipante.EventosInscritos = eventosInscritos;
                }
            }

            return participantesDTO;
        }


        public async Task<ParticipanteDTOResponse> ObterPorIdAsync(int id)
        {
            var participante = await _participanteRepository.ObterPorIdAsync(id);
            if (participante == null) return null;

            return new ParticipanteDTOResponse
            {
                Nome = participante.nome,
                Nascimento = participante.nascimento,
                Cpf = participante.cpf,
                Status_inscricao = participante.status_inscricao,
                Tipo_ingresso = participante.tipo_ingresso,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante")
            };
        }

        public async Task<ParticipanteDTOResponse> CriarAsync(ParticipanteDTORequest participanteDTORequest)
        {
            var participante = new Participante
            {
                nome = participanteDTORequest.Nome,
                nascimento = participanteDTORequest.Nascimento,
                cpf = participanteDTORequest.Cpf,
                status_inscricao = participanteDTORequest.Status_inscricao,
                tipo_ingresso = participanteDTORequest.Tipo_ingresso,
                Login = participanteDTORequest.Login, // Adicionando login
                Senha = participanteDTORequest.Senha  // Adicionando senha
            };

            var criado = await _participanteRepository.AdicionarAsync(participante);

            return new ParticipanteDTOResponse
            {
                Nome = criado.nome,
                Nascimento = criado.nascimento,
                Cpf = criado.cpf,
                Status_inscricao = criado.status_inscricao,
                Tipo_ingresso = criado.tipo_ingresso,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(criado.Id, "Participante")
            };
        }


        public async Task<ParticipanteDTOResponse> AtualizarAsync(int id, ParticipanteDTORequest participanteDTORequest)
        {
            var participante = await _participanteRepository.ObterPorIdAsync(id);

            if (participante == null)
            {
                throw new ArgumentException("Participante não encontrado.");
            }

            participante.nome = participanteDTORequest.Nome;
            participante.nascimento = participanteDTORequest.Nascimento;
            participante.cpf = participanteDTORequest.Cpf;
            participante.status_inscricao = participanteDTORequest.Status_inscricao;
            participante.tipo_ingresso = participanteDTORequest.Tipo_ingresso;

            await _participanteRepository.AtualizarAsync(participante);

            // Retornar o ParticipanteDTOResponse após atualização
            return new ParticipanteDTOResponse
            {
                Nome = participante.nome,
                Nascimento = participante.nascimento,
                Cpf = participante.cpf,
                Status_inscricao = participante.status_inscricao,
                Tipo_ingresso = participante.tipo_ingresso,
                EventosInscritos = await _associacaoEventoPessoa.ObterEventosAsync(participante.Id, "Participante")
            };
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
