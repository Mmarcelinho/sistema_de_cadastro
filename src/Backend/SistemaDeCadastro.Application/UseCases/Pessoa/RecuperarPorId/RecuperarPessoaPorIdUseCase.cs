namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarPorId;

public class RecuperarPessoaPorIdUseCase : IRecuperarPessoaPorIdUseCase
{
    private readonly IPessoaReadOnlyRepositorio _repositorio;

    public RecuperarPessoaPorIdUseCase(IPessoaReadOnlyRepositorio repositorio) => _repositorio = repositorio;

    public async Task<RespostaPessoaJson> Executar(long pessoaId)
    {
        var pessoa = await _repositorio.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        return PessoaMap.ConverterParaResposta(pessoa);
    }
}
