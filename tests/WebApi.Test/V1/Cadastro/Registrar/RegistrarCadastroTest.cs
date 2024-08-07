namespace WebApi.Test.V1.Cadastro.Registrar;

public class RegistrarCadastroTest(CustomWebApplicationFactory webApplicationFactory) : SistemaDeCadastroClassFixture(webApplicationFactory)
{
    private const string METODO = "cadastro";

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var resultado = await DoPost(requestUri: METODO, request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Erro_Email_EmBranco()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();
        var requisicaoEmailVazio = requisicao with { Email = string.Empty };

        var resultado = await DoPost(requestUri: METODO, request: requisicaoEmailVazio);

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

        var resultado = await DoPost(requestUri: METODO, request: requisicaoEmailVazio);

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

        var resultado = await DoPost(requestUri: METODO, request: requisicaoEmailVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = CadastroMensagensDeErro.CADASTRO_NOME_FANTASIA_EMBRANCO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }
}
