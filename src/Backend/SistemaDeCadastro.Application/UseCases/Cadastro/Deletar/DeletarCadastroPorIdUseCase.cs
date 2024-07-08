namespace SistemaDeCadastro.Application.UseCases.Cadastro.Deletar;

public class DeletarCadastroPorIdUseCase : IDeletarCadastroPorIdUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorio;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public DeletarCadastroPorIdUseCase(ICadastroWriteOnlyRepositorio repositorio, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long cadastroId)
    {
        var resultado = await _repositorio.Deletar(cadastroId);

        if (!resultado)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        await _unidadeDeTrabalho.Commit();
    }
}
