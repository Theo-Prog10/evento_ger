namespace eventos_ger.Repository.Interfaces;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILocalRepository
{
    Task<IEnumerable<Local>> ObterLocaisAsync();
    Task<Local> ObterPorIdAsync(int id);
    Task<Local> AdicionarAsync(Local local);
    Task AtualizarAsync(Local local);
    Task DeletarAsync(int id);
    Task<bool> ExisteAsync(int id);
}