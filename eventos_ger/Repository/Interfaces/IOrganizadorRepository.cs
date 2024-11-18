namespace eventos_ger.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;


public interface IOrganizadorRepository
    {
        Task<IEnumerable<Organizador>> ObterTodosAsync(); // Obter todos os organizadores
        Task<Organizador> ObterPorIdAsync(int id); // Obter um organizador por ID
        Task<Organizador> AdicionarAsync(Organizador organizador); // Adicionar um novo organizador
        Task AtualizarAsync(Organizador organizador); // Atualizar os dados de um organizador
        Task DeletarAsync(int id); // Deletar um organizador pelo ID
        Task<bool> ExisteAsync(int id); // Verificar se um organizador existe
    
}
