namespace eventos_ger.Repository.Interfaces;

using eventos_ger.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IParticipanteRepository
    {
        Task<Participante> ObterPorIdAsync(int id);
        Task<IEnumerable<Participante>> ObterTodosAsync();
        Task<Participante> AdicionarAsync(Participante participante);
        Task AtualizarAsync(Participante participante);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
    
}
