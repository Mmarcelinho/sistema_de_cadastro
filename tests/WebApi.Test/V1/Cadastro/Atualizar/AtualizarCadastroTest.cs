namespace WebApi.Test.V1.Cadastro.Atualizar;

public class AtualizarCadastroTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "cadastro";

    private readonly SistemaDeCadastro.Domain.Entidades.Cadastro _cadastro;

    public AtualizarCadastroTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    =>
        _cadastro = webApplicationFactory.Cadastro;

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var resultado = await DoPut(requestUri: $"{METODO}/{_cadastro.Id}", request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Erro_Cadastro_NaoEncontrado()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var resultado = await DoPut(requestUri: $"{METODO}/{100}", request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_Email_EmBranco()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var requisicaoEmailVazio = requisicao with { Email = string.Empty };

        var resultado = await DoPut(requestUri: $"{METODO}/{_cadastro.Id}", request: requisicaoEmailVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = CadastroMensagensDeErro.CADASTRO_EMAIL_EMBRANCO;

        erros.Should().Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_Email_Invalido()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var requisicaoEmailVazio = requisicao with { Email = "abc" };

        var resultado = await DoPut(requestUri: $"{METODO}/{_cadastro.Id}", request: requisicaoEmailVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = CadastroMensagensDeErro.CADASTRO_EMAIL_INVALIDO;

        erros.Should().Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_NomeFantasia_EmBranco()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var requisicaoEmailVazio = requisicao with { NomeFantasia = string.Empty };

        var resultado = await DoPut(requestUri: $"{METODO}/{_cadastro.Id}", request: requisicaoEmailVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = CadastroMensagensDeErro.CADASTRO_NOME_FANTASIA_EMBRANCO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }
}
