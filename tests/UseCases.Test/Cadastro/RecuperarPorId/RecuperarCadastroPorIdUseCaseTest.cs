namespace UseCases.Test.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var resultado = await useCase.Executar(cadastro.Id);

        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(cadastro.Id);
        resultado.Email.Should().Be(cadastro.Email);
    }

    [Fact]
    public async Task Erro_Cadastro_NaoEncontrado()
    {
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastroId: 100);

        await acao.Should().ThrowAsync<NaoEncontradoException>()
        .Where(exception => exception.RecuperarErros().Count == 1 && exception.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO));
    }

    private RecuperarCadastroPorIdUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Cadastro? cadastro = null)
    {
        var repositorio = new CadastroReadOnlyRepositorioBuilder().RecuperarPorId(cadastro).Build();
        var cache = CachingServiceBuilder.Build();
        
        return new RecuperarCadastroPorIdUseCase(repositorio, cache);
    }
}
