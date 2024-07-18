namespace UseCases.Test.Cadastro.Deletar;

public class DeletarCadastroPorIdUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastro.Id);

        await acao.Should().NotThrowAsync();
    }

    [Fact]
    public async Task CadastroNaoEncontrado_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        Func<Task> acao = async () => await useCase.Executar(100);

        var resultado = await acao.Should().ThrowAsync<NaoEncontradoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO));
    }

    private static DeletarCadastroPorIdUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Cadastro cadastro)
    {
        var repositorioWrite = CadastroWriteOnlyRepositorioBuilder.Instancia();

        var repositorioRead = new CadastroReadOnlyRepositorioBuilder().RecuperarPorId(cadastro).Instancia();

        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia();

        return new DeletarCadastroPorIdUseCase(repositorioWrite, repositorioRead, unidadeDeTrabalho);
    }
}
