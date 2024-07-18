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
        resultado.NomeFantasia.Should().Be(cadastro.NomeFantasia);
        resultado.SobrenomeSocial.Should().Be(cadastro.SobrenomeSocial);
        resultado.Empresa.Should().Be(cadastro.Empresa);
    }

    [Fact]
    public async Task CadastroNaoEncontrado_DeveRetornarErro()
    {
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastroId: 100);

        await acao.Should().ThrowAsync<NaoEncontradoException>()
        .Where(exception => exception.RecuperarErros().Count == 1 && exception.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO));
    }

    private static RecuperarCadastroPorIdUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Cadastro cadastro)
    {
        var repositorio = new CadastroReadOnlyRepositorioBuilder().RecuperarPorId(cadastro).Instancia();
        
        return new RecuperarCadastroPorIdUseCase(repositorio);
    }
}
