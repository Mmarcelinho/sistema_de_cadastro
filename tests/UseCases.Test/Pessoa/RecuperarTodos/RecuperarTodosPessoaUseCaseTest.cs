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
        resultado.Pessoas.Should().NotBeNull().And.AllSatisfy(pessoa =>
        {
            pessoa.Id.Should().BeGreaterThan(0);
            pessoa.Email.Should().NotBeNullOrEmpty();
        });
    }

    private RecuperarTodosPessoaUseCase CriarUseCase(List<SistemaDeCadastro.Domain.Entidades.Pessoa> pessoas)
    {
        var repositorio = new PessoaReadOnlyRepositorioBuilder().RecuperarTodos(pessoas).Build();

        return new RecuperarTodosPessoaUseCase(repositorio);
    }
}
