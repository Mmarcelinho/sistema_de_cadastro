namespace UseCases.Test.Cadastro.RecuperarTodos;

public class RecuperarTodosCadastroUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var cadastros = CadastroBuilder.Colecao();

        var useCase = CriarUseCase(cadastros);

        var resultado = await useCase.Executar();

        resultado.Should().NotBeNull();
        resultado.Should().HaveCountGreaterThan(0);
    }

    private RecuperarTodosCadastroUseCase CriarUseCase(List<SistemaDeCadastro.Domain.Entidades.Cadastro> cadastros)
    {
        var repositorio = new CadastroReadOnlyRepositorioBuilder().RecuperarTodos(cadastros).Build();

        return new RecuperarTodosCadastroUseCase(repositorio);
    }
}
