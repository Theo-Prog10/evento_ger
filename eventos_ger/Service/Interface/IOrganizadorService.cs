using eventos_ger.Model.DTOs;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Service.Interface;

public interface IOrganizadorService
{
    // Obter todos os organizadores
    Task<IEnumerable<OrganizadorDTOResponse>> ObterTodosAsync();

    // Obter organizador por ID
    Task<OrganizadorDTOResponse?> ObterPorIdAsync(int id);

    // Criar organizador
    Task<OrganizadorDTOResponse> CriarAsync(OrganizadorDTORequest organizadorDTORequest);

    // Atualizar organizador
    Task<OrganizadorDTOResponse> AtualizarAsync(int id, OrganizadorDTORequest organizadorDTORequest);

    // Remover organizador
    Task RemoverAsync(int id);
}