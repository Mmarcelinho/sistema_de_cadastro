namespace SistemaDeCadastro.Application.UseCases.Pessoa.Deletar;

public class DeletarPessoaPorIdUseCase : IDeletarPessoaPorIdUseCase
{
    private readonly IPessoaWriteOnlyRepositorio _repositorioWrite;

    private readonly IPessoaReadOnlyRepositorio _repositorioRead;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DeletarPessoaPorIdUseCase(IPessoaWriteOnlyRepositorio repositorioWrite, IPessoaReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioWrite = repositorioWrite;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long pessoaId)
    {
        var pessoa = await _repositorioRead.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        await _repositorioWrite.Deletar(pessoaId);
        await _unidadeDeTrabalho.Commit();
    }
}
