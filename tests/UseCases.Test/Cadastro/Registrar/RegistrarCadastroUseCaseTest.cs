namespace UseCases.Test.Cadastro.Registrar;

public class RegistrarCadastroUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var useCase = CriarUseCase();

        var resultado = await useCase.Executar(requisicao);

        resultado.Should().NotBeNull();
        resultado.Email.Should().Be(requisicao.Email);
        resultado.NomeFantasia.Should().Be(requisicao.NomeFantasia);
        resultado.SobrenomeSocial.Should().Be(requisicao.SobrenomeSocial);
        resultado.Empresa.Should().Be(requisicao.Empresa);
    }

    [Fact]
    public async Task CadastroExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var useCase = CriarUseCase(requisicao.Email);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));
    }

    [Fact]
    public async Task DocumentoInvalido_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        requisicao = requisicao with
        {
            Documento = requisicao.Documento with { Numero = "" }
        };

        var useCase = CriarUseCase();

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_NUMERO_EMBRANCO));
    }

    [Fact]
    public async Task CredencialSenhaEmBranco_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        requisicao = requisicao with
        {
            Credencial = requisicao.Credencial with { Senha = "" }
        };

        var useCase = CriarUseCase();

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_CREDENCIAL_SENHA_EMBRANCO));
    }

    private static RegistrarCadastroUseCase CriarUseCase(string? email = null)
    {
        var repositorioWrite = CadastroWriteOnlyRepositorioBuilder.Build();
        var repositorioRead = new CadastroReadOnlyRepositorioBuilder();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();

        if (string.IsNullOrWhiteSpace(email) == false)
            repositorioRead.RecuperarCadastroExistentePorEmail(email);


        return new RegistrarCadastroUseCase(repositorioWrite, repositorioRead.Build(), unidadeDeTrabalho);
    }
}
