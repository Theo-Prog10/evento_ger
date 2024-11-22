using eventos_ger.Model;
using Microsoft.EntityFrameworkCore;
using eventos_ger.Repository.Interfaces;

public class PalestranteRepository : IPalestranteRepository
    {
        private readonly Ger_Evento_Bd _context;

        public PalestranteRepository(Ger_Evento_Bd context)
        {
            _context = context;
        }

        public async Task<Palestrante> ObterPorIdAsync(int id)
        {
            return await _context.Palestrantes.FindAsync(id);
        }

        public async Task<IEnumerable<Palestrante>> ObterTodosAsync()
        {
            return await _context.Palestrantes.ToListAsync();
        }

        public async Task<Palestrante> AdicionarAsync(Palestrante palestrante)
        {
            _context.Palestrantes.Add(palestrante);
            await _context.SaveChangesAsync();
            return palestrante;
        }

        public async Task AtualizarAsync(Palestrante palestrante)
        {
            //Busca o palestrante
            var palestranteExistente = await _context.Palestrantes
                .FirstOrDefaultAsync(p => p.Id == palestrante.Id);

            if (palestranteExistente == null)
            {
                throw new ArgumentException("Palestrante não encontrado.");
            }

            //Atualiza 
            palestranteExistente.nome = palestrante.nome;
            palestranteExistente.biografia = palestrante.biografia;
            palestranteExistente.especialidade = palestrante.especialidade;
            palestranteExistente.cpf = palestrante.cpf;
            palestranteExistente.nascimento = palestrante.nascimento;
            
            _context.Palestrantes.Update(palestranteExistente);
            await _context.SaveChangesAsync();
        }


        public async Task DeletarAsync(int id)
        {
            //busca  pelo ID
            var palestrante = await _context.Palestrantes.FindAsync(id);

            if (palestrante != null)
            {
                var associacoes = await _context.Associacoes
                    .Where(a => a.Id == palestrante.Id && a.tipo_pessoa == "Palestrante")
                    .ToListAsync();
                
                _context.Associacoes.RemoveRange(associacoes);
                
                //salva as alterações nos eventos
                await _context.SaveChangesAsync();

                //remove o palestrante do banco de dados
                _context.Palestrantes.Remove(palestrante);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Palestrantes.AnyAsync(p => p.Id == id);
        }
    
}