namespace UseCases.Test.Pessoa.RecuperarPorId;

public class RecuperarPessoaPorIdUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(pessoa);

        var resultado = await useCase.Executar(pessoa.Id);

        resultado.Should().NotBeNull();
        resultado.Cpf.Should().Be(pessoa.Cpf);
        resultado.Cnpj.Should().Be(pessoa.Cnpj);
        resultado.Nome.Should().Be(pessoa.Nome);
        resultado.NomeFantasia.Should().Be(pessoa.NomeFantasia);
        resultado.Email.Should().Be(pessoa.Email);
        resultado.Nascimento.Should().Be(pessoa.Nascimento);
    }

    [Fact]
    public async Task Erro_Cadastro_NaoEncontrado()
    {
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(pessoa);

        var acao = async () => await useCase.Executar(pessoaId: 100);

        await acao.Should().ThrowAsync<NaoEncontradoException>()
        .Where(exception => exception.RecuperarErros().Count == 1 && exception.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO));
    }

    private static RecuperarPessoaPorIdUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Pessoa? pessoa = null)
    {
        var repositorio = new PessoaReadOnlyRepositorioBuilder().RecuperarPorId(pessoa).Build();
        
        return new RecuperarPessoaPorIdUseCase(repositorio);
    }
}
