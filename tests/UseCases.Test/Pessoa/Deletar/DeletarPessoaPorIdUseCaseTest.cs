namespace UseCases.Test.Pessoa.Deletar;

public class DeletarPessoaPorIdUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var Pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(Pessoa);

        var acao = async () => await useCase.Executar(Pessoa.Id);

        await acao.Should().NotThrowAsync();
    }

    [Fact]
    public async Task PessoaNaoEncontrado_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var Pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(Pessoa);

        Func<Task> acao = async () => await useCase.Executar(100);

        var resultado = await acao.Should().ThrowAsync<NaoEncontradoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO));
    }

    private static DeletarPessoaPorIdUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Pessoa Pessoa)
    {
        var repositorioWrite = PessoaWriteOnlyRepositorioBuilder.Instancia();

        var repositorioRead = new PessoaReadOnlyRepositorioBuilder().RecuperarPorId(Pessoa).Instancia();

        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia();

        return new DeletarPessoaPorIdUseCase(repositorioWrite, repositorioRead, unidadeDeTrabalho);
    }
}
