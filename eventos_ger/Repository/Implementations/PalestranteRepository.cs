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
            _context.Palestrantes.Update(palestrante);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            // Busca o palestrante pelo ID
            var palestrante = await _context.Palestrantes.FindAsync(id);

            if (palestrante != null)
            {
                // Carrega os eventos associados ao palestrante
                var eventos = await _context.Eventos
                    .Where(e => e.palestrantes_presentes.Contains(id))
                    .ToListAsync();

                // Remove o ID do palestrante das listas de palestrantes dos eventos
                foreach (var evento in eventos)
                {
                    evento.palestrantes_presentes.Remove(id);
                }

                // Salva as alterações nos eventos
                await _context.SaveChangesAsync();

                // Remove o palestrante do banco de dados
                _context.Palestrantes.Remove(palestrante);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Palestrantes.AnyAsync(p => p.Id == id);
        }
    
}