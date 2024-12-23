using eventos_ger.Model;
using eventos_ger.Model.DTOs;

namespace eventos_ger.Service.Interface;

public interface ILocalService
{
    Task<IEnumerable<Local>> ObterLocaisAsync();
    Task<LocalDTO> ObterPorIdAsync(int id);
    Task<Local> AdicionarAsync(LocalDTO localDto);
    Task AtualizarAsync(int id, LocalDTO localDto);
    Task DeletarAsync(int id);
}