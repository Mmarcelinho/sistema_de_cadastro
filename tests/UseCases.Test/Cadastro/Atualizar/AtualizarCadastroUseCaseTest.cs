namespace UseCases.Test.Cadastro.Atualizar;

public class AtualizarCadastroUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastro.Id, requisicao);

        await acao.Should().NotThrowAsync();

        cadastro.Email.Should().Be(requisicao.Email);
        cadastro.NomeFantasia.Should().Be(requisicao.NomeFantasia);
        cadastro.SobrenomeSocial.Should().Be(requisicao.SobrenomeSocial);
        cadastro.Empresa.Should().Be(requisicao.Empresa);
    }

    [Fact]
    public async Task CadastroExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro, requisicao.Email);

        Func<Task> acao = async () => await useCase.Executar(cadastro.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));
    }

    [Fact]
    public async Task CadastroNaoEncontrado_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        Func<Task> acao = async () => await useCase.Executar(100, requisicao);

        var resultado = await acao.Should().ThrowAsync<NaoEncontradoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO));
    }

    [Fact]
    public async Task DocumentoInvalido_DeveRetornarErro()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        requisicao = requisicao with
        {
            Documento = requisicao.Documento with { Numero = "" }
        };
        
        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastro.Id, requisicao);

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

        var cadastro = CadastroBuilder.Instancia();

        var useCase = CriarUseCase(cadastro);

        var acao = async () => await useCase.Executar(cadastro.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_CREDENCIAL_SENHA_EMBRANCO));
    }

    private static AtualizarCadastroUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Cadastro? cadastro = null, string? email = null)
    {
        var repositorioUpdate = new CadastroUpdateOnlyRepositorioBuilder().RecuperarPorId(cadastro).Instancia();

        var repositorioRead = new CadastroReadOnlyRepositorioBuilder();

        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia();

        if (string.IsNullOrWhiteSpace(email) == false)
            repositorioRead.RecuperarCadastroExistentePorEmail(email);

        return new AtualizarCadastroUseCase(repositorioUpdate, repositorioRead.Instancia(), unidadeDeTrabalho);
    }
}
