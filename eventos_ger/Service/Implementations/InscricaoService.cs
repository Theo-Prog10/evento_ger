using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace eventos_ger.Service
{
    public class InscricaoService : IInscricaoService
    {
        private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;
        private readonly IEventoRepository _eventoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IPalestranteRepository _palestranteRepository;

        public InscricaoService(IAssociacaoEventoPessoa associacaoEventoPessoa, 
                                IEventoRepository eventoRepository, 
                                IParticipanteRepository participanteRepository, 
                                IPalestranteRepository palestranteRepository)
        {
            _associacaoEventoPessoa = associacaoEventoPessoa;
            _eventoRepository = eventoRepository;
            _participanteRepository = participanteRepository;
            _palestranteRepository = palestranteRepository;
        }

        public async Task<IActionResult> AddParticipanteAsync(int eventoId, int participanteId)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
            if (evento == null) return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });

            var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
            if (participante == null) return new NotFoundObjectResult(new { mensagem = "Participante não encontrado." });

            if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, participanteId, "Participante") != null)
                return new ConflictObjectResult(new { mensagem = "O participante já está inscrito neste evento." });

            var associacao = new AssociacaoEventoPessoa
            {
                idEvento = eventoId,
                idPessoa = participanteId,
                tipo_pessoa = "Participante"
            };

            await _associacaoEventoPessoa.AdicionarAsync(associacao);
            return new OkResult();
        }

        public async Task<IActionResult> AddPalestranteAsync(int eventoId, int palestranteId)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
            if (evento == null) return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });

            var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
            if (palestrante == null) return new NotFoundObjectResult(new { mensagem = "Palestrante não encontrado." });

            if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, palestranteId, "Palestrante") != null)
                return new ConflictObjectResult(new { mensagem = "O palestrante já está inscrito neste evento." });

            var associacao = new AssociacaoEventoPessoa
            {
                idEvento = eventoId,
                idPessoa = palestranteId,
                tipo_pessoa = "Palestrante"
            };

            await _associacaoEventoPessoa.AdicionarAsync(associacao);
            return new OkResult();
        }

        public async Task<IActionResult> DeleteParticipanteEventoAsync(int participanteId, int eventoId)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
            if (evento == null) return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });

            var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
            if (participante == null) return new NotFoundObjectResult(new { mensagem = "Participante não encontrado." });

            var associacao = new AssociacaoEventoPessoa
            {
                idEvento = eventoId,
                idPessoa = participanteId,
                tipo_pessoa = "Participante"
            };

            await _associacaoEventoPessoa.RemoverAsync(associacao);
            return new OkResult();
        }

        public async Task<IActionResult> DeletePalestranteEventoAsync(int palestranteId, int eventoId)
        {
            var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
            if (evento == null) return new NotFoundObjectResult(new { mensagem = "Evento não encontrado." });

            var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
            if (palestrante == null) return new NotFoundObjectResult(new { mensagem = "Palestrante não encontrado." });

            var associacao = new AssociacaoEventoPessoa
            {
                idEvento = eventoId,
                idPessoa = palestranteId,
                tipo_pessoa = "Palestrante"
            };

            await _associacaoEventoPessoa.RemoverAsync(associacao);
            return new OkResult();
        }
    }
}
