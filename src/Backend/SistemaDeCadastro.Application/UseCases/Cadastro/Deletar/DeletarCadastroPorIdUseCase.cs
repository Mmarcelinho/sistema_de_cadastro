namespace SistemaDeCadastro.Application.UseCases.Cadastro.Deletar;

public class DeletarCadastroPorIdUseCase(ICadastroWriteOnlyRepositorio repositorioWrite, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho) : IDeletarCadastroPorIdUseCase
{
    public async Task Executar(long cadastroId)
    {
        var cadastro = await repositorioRead.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        await repositorioWrite.Deletar(cadastroId);

        await unidadeDeTrabalho.Commit();
    }
}
