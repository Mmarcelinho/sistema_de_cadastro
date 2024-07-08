namespace SistemaDeCadastro.Application.UseCases.Pessoa.Deletar;

public class DeletarPessoaIdUseCase : IDeletarPessoaPorIdUseCase
{
    private readonly IPessoaWriteOnlyRepositorio _repositorio;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DeletarPessoaIdUseCase(IPessoaWriteOnlyRepositorio repositorio, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long pessoaId)
    {
        var resultado = await _repositorio.Deletar(pessoaId);

        if (resultado == false)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        await _unidadeDeTrabalho.Commit();
    }
}
