namespace eventos_ger.Repository.Interfaces;

using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPalestranteRepository
    {
        Task<Palestrante> ObterPorIdAsync(int id);
        Task<IEnumerable<Palestrante>> ObterTodosAsync();
        Task<Palestrante> AdicionarAsync(Palestrante palestrante);
        Task AtualizarAsync(Palestrante palestrante);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
    
}
