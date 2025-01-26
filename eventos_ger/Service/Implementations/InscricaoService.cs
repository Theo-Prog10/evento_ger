using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;

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

    public async Task<bool> AddParticipanteAsync(int eventoId, int participanteId)
    {
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return false;

        var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
        if (participante == null) return false;

        if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, participanteId, "Participante") != null)
            return false;

        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = participanteId,
            tipo_pessoa = "Participante"
        };

        await _associacaoEventoPessoa.AdicionarAsync(associacao);
        return true;
    }

    public async Task<bool> AddPalestranteAsync(int eventoId, int palestranteId)
    {
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return false;

        var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
        if (palestrante == null) return false;

        if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, palestranteId, "Palestrante") != null)
            return false;

        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = palestranteId,
            tipo_pessoa = "Palestrante"
        };

        await _associacaoEventoPessoa.AdicionarAsync(associacao);
        return true;
    }

    public async Task<bool> DeleteParticipanteEventoAsync(int participanteId, int eventoId)
    {
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return false;

        var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
        if (participante == null) return false;

        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = participanteId,
            tipo_pessoa = "Participante"
        };

        await _associacaoEventoPessoa.RemoverAsync(associacao);
        return true;
    }

    public async Task<bool> DeletePalestranteEventoAsync(int palestranteId, int eventoId)
    {
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return false;

        var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
        if (palestrante == null) return false;

        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = palestranteId,
            tipo_pessoa = "Palestrante"
        };

        await _associacaoEventoPessoa.RemoverAsync(associacao);
        return true;
    }
}
