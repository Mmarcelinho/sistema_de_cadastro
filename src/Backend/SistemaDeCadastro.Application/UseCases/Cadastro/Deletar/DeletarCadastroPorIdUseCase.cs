namespace SistemaDeCadastro.Application.UseCases.Cadastro.Deletar;

public class DeletarCadastroPorIdUseCase : IDeletarCadastroPorIdUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioWrite;

    private readonly ICadastroReadOnlyRepositorio _repositorioRead;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DeletarCadastroPorIdUseCase(ICadastroWriteOnlyRepositorio repositorioWrite, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioWrite = repositorioWrite;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long cadastroId)
    {
        var cadastro = await _repositorioRead.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        await _repositorioWrite.Deletar(cadastroId);

        await _unidadeDeTrabalho.Commit();
    }
}
