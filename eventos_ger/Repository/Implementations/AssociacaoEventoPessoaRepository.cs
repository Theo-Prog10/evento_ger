using Microsoft.EntityFrameworkCore;
using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Repository;

public class AssociacaoEventoPessoaRepository : IAssociacaoEventoPessoa
{
    private readonly Ger_Evento_Bd _context;

    public AssociacaoEventoPessoaRepository(Ger_Evento_Bd context)
    {
        _context = context;
    }

    public async Task<AssociacaoEventoPessoa> ObterAssociacaoAsync(int idEvento, int idPessoa, string tipo_pessoa)
    {
        var aux = await _context.Associacoes
            .FirstOrDefaultAsync(a =>
                a.idEvento == idEvento &&
                a.idPessoa == idPessoa &&
                a.tipo_pessoa == tipo_pessoa);
        return aux;
    }
    
    
    public async Task<List<int>> ObterEventosAsync(int id, string tipo_pessoa)
    {
        return await _context.Associacoes
            .Where(a => a.idPessoa == id && a.tipo_pessoa == tipo_pessoa)
            .Select(a => a.idEvento)
            .ToListAsync();
    }


    public async Task<List<int>> ObterPessoasAsync(int idEvento, string tipo_pessoa)
    {
        return await _context.Associacoes
            .Where(a => 
                a.idEvento == idEvento && 
                a.tipo_pessoa == tipo_pessoa)
            .Select(a => 
                a.idPessoa)
            .ToListAsync();
    }

    public async Task<AssociacaoEventoPessoa> AdicionarAsync(AssociacaoEventoPessoa associacao)
    {
        _context.Associacoes.Add(associacao);
        await _context.SaveChangesAsync();
        return associacao;
    }

    public async Task<AssociacaoEventoPessoa> RemoverAsync(AssociacaoEventoPessoa associacao)
    {
        //Busca a entidade correspondente
        var associacaoExistente = await ObterAssociacaoAsync(associacao.idEvento, associacao.idPessoa, associacao.tipo_pessoa);

        if (associacaoExistente == null) return null;
        
        _context.Associacoes.Remove(associacaoExistente);
        await _context.SaveChangesAsync();
        return null;
    }
}