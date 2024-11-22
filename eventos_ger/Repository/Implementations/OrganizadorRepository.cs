namespace eventos_ger.Repository;

using Model;
using Interfaces;
using Microsoft.EntityFrameworkCore;


public class OrganizadorRepository : IOrganizadorRepository
    {
        private readonly Ger_Evento_Bd _context;

        public OrganizadorRepository(Ger_Evento_Bd context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organizador>> ObterTodosAsync()
        {
            return await _context.Organizadores.ToListAsync();
        }

        public async Task<Organizador> ObterPorIdAsync(int id)
        {
            return await _context.Organizadores
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        

        public async Task<Organizador> AdicionarAsync(Organizador organizador)
        {
            _context.Organizadores.Add(organizador);
            await _context.SaveChangesAsync();
            return organizador;
        }

        public async Task AtualizarAsync(Organizador organizador)
        {
            var organizadorExistente = await _context.Organizadores
                .FirstOrDefaultAsync(o => o.Id == organizador.Id);
            if (organizadorExistente == null)
            {
                throw new ArgumentException("Organizador não encontrado.");
            }

            //Atualizando
            organizadorExistente.nome = organizador.nome;
            organizadorExistente.cpf = organizador.cpf;
            organizadorExistente.nascimento = organizador.nascimento;

            _context.Organizadores.Update(organizadorExistente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var organizador = await ObterPorIdAsync(id);

            if (organizador == null)
            {
                throw new ArgumentException("Organizador não encontrado.");
            }

            var associacoes = _context.Associacoes
                .Where(a => a.idPessoa == id && a.tipo_pessoa == "Organizador")
                .ToListAsync();
            
            //Verificando se o organizador está associado a algum evento
            if (associacoes != null)
            {
                throw new InvalidOperationException("O organizador não pode ser excluído porque está associado a eventos.");
            }

            //Remove se ele nao estiver em um evento
            _context.Organizadores.Remove(organizador);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Organizadores.AnyAsync(o => o.Id == id);
        }
    
}
