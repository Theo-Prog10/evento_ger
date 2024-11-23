using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
namespace eventos_ger.Controller;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ControllerInscricao : ControllerBase
{
    private readonly IAssociacaoEventoPessoa _associacaoEventoPessoa;
    private readonly IEventoRepository _eventoRepository;
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IPalestranteRepository _palestranteRepository;
    
    

    public ControllerInscricao(IAssociacaoEventoPessoa associacaoEventoPessoa, IEventoRepository eventoRepository, IParticipanteRepository participanteRepository, IPalestranteRepository palestranteRepository)
    {
        _associacaoEventoPessoa = associacaoEventoPessoa;
        _eventoRepository = eventoRepository;
        _participanteRepository = participanteRepository;
        _palestranteRepository = palestranteRepository;
    }
    
    //inscreve participante em evento
    [HttpPost("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> AddParticipante(int eventoId, int participanteId)
    {
        //Verifica se o evento existe
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return NotFound(new { mensagem = "Evento não encontrado." });
        
        //Verifica se o participante existe
        var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
        if (participante == null)return NotFound(new { mensagem = "Participante não encontrado." });
        
        // Verifica se associação já existe
        if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, participanteId, "Participante") != null)
            return Conflict(new { mensagem = "O participante já está inscrito neste evento." });
        
        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = participanteId,
            tipo_pessoa = "Participante"
        };
        
        return Ok(_associacaoEventoPessoa.AdicionarAsync(associacao));
    }
    
    //adiciona palestrante em evento
    [HttpPost("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> AddPalestrante(int eventoId, int palestranteId)
    {
        //Verifica se o evento existe
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return NotFound(new { mensagem = "Evento não encontrado." });
        
        //Verifica se o palestrante existe
        var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
        if (palestrante == null) return NotFound(new { mensagem = "Palestrante não encontrado." });
        
        //Verifica se associação já existe
        if (await _associacaoEventoPessoa.ObterAssociacaoAsync(eventoId, palestranteId, "Palestrante") != null)
            return Conflict(new { mensagem = "O palestrante já está inscrito neste evento." });
        
        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = palestranteId,
            tipo_pessoa = "Palestrante"
        };
        
        return Ok(_associacaoEventoPessoa.AdicionarAsync(associacao));
    }
    //Remove participante do evento
    [HttpDelete("evento/{eventoId}/participantes/{participanteId}")]
    public async Task<IActionResult> DeleteParticipanteEvento(int participanteId, int eventoId)
    {
        //Verifica se o evento existe
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return NotFound(new { mensagem = "Evento não encontrado." });
        
        //Verifica se o participante existe
        var participante = await _participanteRepository.ObterPorIdAsync(participanteId);
        if (participante == null) return NotFound(new { mensagem = "Participante não encontrado." });
        
        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = participanteId,
            tipo_pessoa = "Participante"
        };
        return Ok(_associacaoEventoPessoa.RemoverAsync(associacao));
    }
    
    //Remove palestrante do evento
    [HttpDelete("evento/{eventoId}/palestrantes/{palestranteId}")]
    public async Task<IActionResult> DeletePalestranteEvento(int palestranteId, int eventoId)
    {
        //Verifica se o evento existe
        var evento = await _eventoRepository.ObterPorIdAsync(eventoId);
        if (evento == null) return NotFound(new { mensagem = "Evento não encontrado." });
        
        //Verifica se o palestrante existe
        var palestrante = await _palestranteRepository.ObterPorIdAsync(palestranteId);
        if (palestrante == null) return NotFound(new { mensagem = "Palestrante não encontrado." });
        
        var associacao = new AssociacaoEventoPessoa
        {
            idEvento = eventoId,
            idPessoa = palestranteId,
            tipo_pessoa = "Palestrante"
        };
        return Ok(_associacaoEventoPessoa.RemoverAsync(associacao));
    }
}