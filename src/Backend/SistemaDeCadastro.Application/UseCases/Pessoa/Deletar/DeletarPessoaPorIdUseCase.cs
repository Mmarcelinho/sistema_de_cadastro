namespace SistemaDeCadastro.Application.UseCases.Pessoa.Deletar;

public class DeletarPessoaPorIdUseCase(IPessoaWriteOnlyRepositorio repositorioWrite, IPessoaReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho) : IDeletarPessoaPorIdUseCase
{
    public async Task Executar(long pessoaId)
    {
        var pessoa = await repositorioRead.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        await repositorioWrite.Deletar(pessoaId);
        await unidadeDeTrabalho.Commit();
    }
}
