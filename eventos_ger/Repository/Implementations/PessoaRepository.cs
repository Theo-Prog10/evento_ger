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

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pessoa> AdicionarAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task AtualizarAsync(Pessoa pessoa)
        {
            //Buscar o participante
            var pessoaExistente = await _context.Pessoas
                .FirstOrDefaultAsync(p => p.Id == pessoa.Id);

            if (pessoaExistente == null)
            {
                throw new ArgumentException("Participante não encontrado.");
            }

            //Atualizando
            pessoaExistente.nome = pessoa.nome;
            pessoaExistente.nascimento = pessoa.nascimento;
            pessoaExistente.cpf = pessoa.cpf;
            
            _context.Pessoas.Update(pessoaExistente);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeletarAsync(int id) //revisar se continua valido ou se é melhor desativar usuario
        {
            //Busca pelo ID
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa != null)
            {
                var associacoes = await _context.Associacoes
                    .Where(a => a.Id == pessoa.Id && a.tipo_pessoa == "Participante")
                    .ToListAsync();
                    
                _context.Associacoes.RemoveRange(associacoes);
                
                await _context.SaveChangesAsync();

                // Remove o participante do banco de dados
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Pessoas.AnyAsync(p => p.Id == id);
        }
        
        public async Task<Pessoa?> ObterPorLoginSenhaAsync(string login, string senha)
        {
            return await _context.Pessoas
                .Join(_context.Usuarios, 
                    pessoa => pessoa.id_usuario, 
                    usuario => usuario.Id, 
                    (pessoa, usuario) => new { Pessoa = pessoa, Usuario = usuario })
                .Where(joined => joined.Usuario.login == login && joined.Usuario.senha == senha)
                .Select(joined => joined.Pessoa)
                .FirstOrDefaultAsync();
        }





        
    }
}