using eventos_ger.Model;
using eventos_ger.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eventos_ger.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly Ger_Evento_Bd _context;

        public UsuarioRepository(Ger_Evento_Bd context)
        {
            _context = context;
        }
        
        public async Task<Pessoa?> ObterPorLoginESenhaAsync(string login, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(p => p.login == login && p.senha == senha);
            if (usuario != null)
                return await _context.Pessoas.FirstOrDefaultAsync(p => p.id_usuario == usuario.Id);
            return null;
        }

        public async Task<IEnumerable<string?>> ObterLoginsAsync()
        {
            return await _context.Usuarios
                .Select(p => p.login)
                .ToListAsync();
        }

        public async Task<string?> ObterLoginAsync(int id)
        {
            return await _context.Usuarios
                .Where(p => p.Id == id)
                .Select(p => p.login)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            //Buscar o participante
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(p => p.Id == usuario.Id);

            if (usuarioExistente == null)
            {
                throw new ArgumentException("Usuario não encontrado.");
            }

            //Atualizando
            usuarioExistente.login = usuario.login;
            usuarioExistente.senha = usuario.senha;
            
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeletarAsync(int id) //revisar se continua valido ou se é melhor desativar usuario
        {
            //Busca pelo ID
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(p => p.Id == id);
        }
}