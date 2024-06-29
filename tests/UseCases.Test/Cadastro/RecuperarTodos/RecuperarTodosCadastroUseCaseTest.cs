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
        resultado.Cadastros.Should().NotBeNull().And.AllSatisfy(cadastro =>
        {
            cadastro.Id.Should().BeGreaterThan(0);
            cadastro.Email.Should().NotBeNullOrEmpty();
        });
    }

    private RecuperarTodosCadastroUseCase CriarUseCase(List<SistemaDeCadastro.Domain.Entidades.Cadastro> cadastros)
    {
        var repositorio = new CadastroReadOnlyRepositorioBuilder().RecuperarTodos(cadastros).Build();

        return new RecuperarTodosCadastroUseCase(repositorio);
    }
}
