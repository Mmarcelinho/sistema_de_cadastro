namespace UseCases.Test.Pessoa.RecuperarTodos;

public class RecuperarTodosPessoaUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var pessoas = PessoaBuilder.Colecao();

        var useCase = CriarUseCase(pessoas);

        var resultado = await useCase.Executar();

        resultado.Should().NotBeNull();
        resultado.Should().HaveCountGreaterThan(0);
    }

    private RecuperarTodosPessoaUseCase CriarUseCase(List<SistemaDeCadastro.Domain.Entidades.Pessoa> pessoas)
    {
        var repositorio = new PessoaReadOnlyRepositorioBuilder().RecuperarTodos(pessoas).Build();

        return new RecuperarTodosPessoaUseCase(repositorio);
    }
}
