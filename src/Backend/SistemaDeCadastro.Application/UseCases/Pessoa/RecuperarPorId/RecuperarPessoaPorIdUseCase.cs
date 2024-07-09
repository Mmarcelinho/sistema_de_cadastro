using SistemaDeCadastro.Application.Mappings;

namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarPorId;

public class RecuperarPessoaPorIdUseCase : IRecuperarPessoaPorIdUseCase
{
    private readonly IPessoaReadOnlyRepositorio _repositorio;
    private readonly ICachingService _cache;

    public RecuperarPessoaPorIdUseCase(IPessoaReadOnlyRepositorio repositorio, ICachingService cache)
    {
        _repositorio = repositorio;
        _cache = cache;
    }

    public async Task<RespostaPessoaJson> Executar(long pessoaId)
    {
        string? pessoaCache = await _cache.Recuperar(pessoaId.ToString());
        Domain.Entidades.Pessoa? pessoa;

        if (string.IsNullOrEmpty(pessoaCache))
        {
            pessoa = await _repositorio.RecuperarPorId(pessoaId);

            if (pessoa is null)
                throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

            await _cache.Registrar(pessoaId.ToString(), JsonConvert.SerializeObject(pessoa));

            return PessoaMap.ConverterParaResposta(pessoa);
        }

        pessoa = JsonConvert.DeserializeObject<Domain.Entidades.Pessoa>(pessoaCache);

        return PessoaMap.ConverterParaResposta(pessoa);
    }

}
