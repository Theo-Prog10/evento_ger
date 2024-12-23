using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;
using eventos_ger.Service.Interface;

public class LocalService : ILocalService
{
    private readonly ILocalRepository _localRepository;

    public LocalService(ILocalRepository localRepository)
    {
        _localRepository = localRepository;
    }

    public async Task<IEnumerable<Local>> ObterLocaisAsync()
    {
        return await _localRepository.ObterLocaisAsync();
    }

    public async Task<LocalDTO> ObterPorIdAsync(int id)
    {
        var local = await _localRepository.ObterPorIdAsync(id);
        if (local == null) return null;

        var localDto = new LocalDTO
        {
            Id = local.Id,
            Nome = local.nome,
            Logradouro = local.Logradouro,
            Numero = local.Numero,
            UF = local.UF,
            Cidade = local.Cidade,
            Bairro = local.Bairro
        };

        return localDto;
    }

    public async Task<Local> AdicionarAsync(LocalDTO localDto)
    {
        var local = new Local
        {
            nome = localDto.Nome,
            Logradouro = localDto.Logradouro,
            Numero = localDto.Numero,
            UF = localDto.UF,
            Cidade = localDto.Cidade,
            Bairro = localDto.Bairro
        };

        return await _localRepository.AdicionarAsync(local);
    }

    public async Task AtualizarAsync(int id, LocalDTO localDto)
    {
        var localExistente = await _localRepository.ObterPorIdAsync(id);
        if (localExistente == null) throw new KeyNotFoundException("Local não encontrado.");

        localExistente.nome = localDto.Nome;
        localExistente.Logradouro = localDto.Logradouro;
        localExistente.Bairro = localDto.Bairro;
        localExistente.Cidade = localDto.Cidade;
        localExistente.UF = localDto.UF;
        localExistente.Numero = localDto.Numero;

        await _localRepository.AtualizarAsync(localExistente);
    }

    public async Task DeletarAsync(int id)
    {
        var local = await _localRepository.ObterPorIdAsync(id);
        if (local == null) throw new KeyNotFoundException("Local não encontrado.");

        await _localRepository.DeletarAsync(id);
    }
}
