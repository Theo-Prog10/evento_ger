using eventos_ger.Model.DTOs;

namespace eventos_ger.Service.Interface;

public interface IOrganizadorService
{
    Task<IEnumerable<OrganizadorDTO>> ObterTodosAsync();
    Task<OrganizadorDTO?> ObterPorIdAsync(int id);
    Task<OrganizadorDTO> CriarAsync(OrganizadorDTO organizadorDTO);
    Task AtualizarAsync(int id, OrganizadorDTO organizadorDTO);
    Task RemoverAsync(int id);
}