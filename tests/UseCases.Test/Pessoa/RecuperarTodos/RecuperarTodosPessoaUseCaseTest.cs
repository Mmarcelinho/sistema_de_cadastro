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
            pessoa.Cpf.Should().NotBeNullOrEmpty();
            pessoa.Cnpj.Should().NotBeNullOrEmpty();
            pessoa.Nome.Should().NotBeNullOrEmpty();
            pessoa.NomeFantasia.Should().NotBeNullOrEmpty();
            pessoa.Email.Should().NotBeNullOrEmpty();
        });
    }

    private static RecuperarTodosPessoaUseCase CriarUseCase(List<SistemaDeCadastro.Domain.Entidades.Pessoa> pessoas)
    {
        var repositorio = new PessoaReadOnlyRepositorioBuilder().RecuperarTodos(pessoas).Build();

        return new RecuperarTodosPessoaUseCase(repositorio);
    }
}
