using eventos_ger.Model;
using eventos_ger.Model.DTOs.Response;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;

public class LocalService : ILocalService
{
    private readonly ILocalRepository _localRepository;

    public LocalService(ILocalRepository localRepository)
    {
        _localRepository = localRepository;
    }

    public async Task<IEnumerable<LocalDTOResponse>> ObterLocaisAsync()
    {
        var locais = await _localRepository.ObterLocaisAsync();
        var locaisDto = locais.Select(l => new LocalDTOResponse
        {
            Id = l.Id,
            Nome = l.nome,
            Logradouro = l.Logradouro,
            Numero = l.Numero,
            UF = l.UF,
            Cidade = l.Cidade,
            Bairro = l.Bairro
        }).ToList();

        return locaisDto;
    }

    public async Task<LocalDTOResponse> ObterPorIdAsync(int id)
    {
        var local = await _localRepository.ObterPorIdAsync(id);
        if (local == null) return null;

        return new LocalDTOResponse
        {
            Id = local.Id,
            Nome = local.nome,
            Logradouro = local.Logradouro,
            Numero = local.Numero,
            UF = local.UF,
            Cidade = local.Cidade,
            Bairro = local.Bairro
        };
    }

    public async Task<LocalDTOResponse> AdicionarAsync(LocalDTORequest localDtoRequest)
    {
        var local = new Local
        {
            nome = localDtoRequest.Nome,
            Logradouro = localDtoRequest.Logradouro,
            Numero = localDtoRequest.Numero,
            UF = localDtoRequest.UF,
            Cidade = localDtoRequest.Cidade,
            Bairro = localDtoRequest.Bairro
        };

        var localCriado = await _localRepository.AdicionarAsync(local);

        return new LocalDTOResponse
        {
            Id = localCriado.Id,
            Nome = localCriado.nome,
            Logradouro = localCriado.Logradouro,
            Numero = localCriado.Numero,
            UF = localCriado.UF,
            Cidade = localCriado.Cidade,
            Bairro = localCriado.Bairro
        };
    }

    public async Task<LocalDTOResponse> AtualizarAsync(int id, LocalDTORequest localDtoRequest)
    {
        var localExistente = await _localRepository.ObterPorIdAsync(id);
        if (localExistente == null) throw new KeyNotFoundException("Local não encontrado.");

        localExistente.nome = localDtoRequest.Nome;
        localExistente.Logradouro = localDtoRequest.Logradouro;
        localExistente.Bairro = localDtoRequest.Bairro;
        localExistente.Cidade = localDtoRequest.Cidade;
        localExistente.UF = localDtoRequest.UF;
        localExistente.Numero = localDtoRequest.Numero;

        await _localRepository.AtualizarAsync(localExistente);

        // Retorna o DTO atualizado
        return new LocalDTOResponse
        {
            Id = localExistente.Id,
            Nome = localExistente.nome,
            Logradouro = localExistente.Logradouro,
            Numero = localExistente.Numero,
            UF = localExistente.UF,
            Cidade = localExistente.Cidade,
            Bairro = localExistente.Bairro
        };
    }


    public async Task DeletarAsync(int id)
    {
        var local = await _localRepository.ObterPorIdAsync(id);
        if (local == null) throw new KeyNotFoundException("Local não encontrado.");

        await _localRepository.DeletarAsync(id);
    }
}
