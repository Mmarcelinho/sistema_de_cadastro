namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarPorId;

public class RecuperarPessoaPorIdUseCase(IPessoaReadOnlyRepositorio repositorio) : IRecuperarPessoaPorIdUseCase
{
    public async Task<RespostaPessoaJson> Executar(long pessoaId)
    {
        var pessoa = await repositorio.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        return PessoaMap.ConverterParaResposta(pessoa);
    }
}
