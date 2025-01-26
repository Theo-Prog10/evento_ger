using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eventos_ger.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly Ger_Evento_Bd _context;

        public PessoaRepository(Ger_Evento_Bd context)
        {
            _context = context;
        }

        // Método assíncrono para consultar qualquer tipo de Pessoa (Participante, Organizador, Palestrante)
        public async Task<Pessoa?> ObterPorLoginESenhaAsync(string login, string senha)
        {
            // Verifica cada tabela específica (Participante, Organizador, Palestrante)
            var participante = await _context.Participantes
                .FirstOrDefaultAsync(p => p.Login == login && p.Senha == senha);
            if (participante != null)
                return participante;

            var organizador = await _context.Organizadores
                .FirstOrDefaultAsync(o => o.Login == login && o.Senha == senha);
            if (organizador != null)
                return organizador;

            var palestrante = await _context.Palestrantes
                .FirstOrDefaultAsync(p => p.Login == login && p.Senha == senha);
            if (palestrante != null)
                return palestrante;

            return null; // Retorna null caso não encontre em nenhuma das tabelas
        }
    }
}