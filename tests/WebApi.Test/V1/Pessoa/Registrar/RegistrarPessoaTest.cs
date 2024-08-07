namespace WebApi.Test.V1.Pessoa.Registrar;

public class RegistrarPessoaTest(CustomWebApplicationFactory webApplicationFactory) : SistemaDeCadastroClassFixture(webApplicationFactory)
{
    private const string METODO = "pessoa";

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var resultado = await DoPost(requestUri: METODO, request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Erro_Cpf_EmBranco()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoCpfVazio = requisicao with { Cpf = string.Empty };

        var resultado = await DoPost(requestUri: METODO, request: requisicaoCpfVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_CPF_EMBRANCO;

        erros.Should().Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_Nome_EmBranco()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoNomeVazio = requisicao with { Nome = string.Empty };

        var resultado = await DoPost(requestUri: METODO, request: requisicaoNomeVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_NOME_EMBRANCO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_NomeFantasia_EmBranco()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoNomeFantasiaVazio = requisicao with { NomeFantasia = string.Empty };

        var resultado = await DoPost(requestUri: METODO, request: requisicaoNomeFantasiaVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_NOME_FANTASIA_EMBRANCO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_Nascimento_Invalido()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoNascimentoInvalido = requisicao with { Nascimento = DateTime.MinValue };

        var resultado = await DoPost(requestUri: METODO, request: requisicaoNascimentoInvalido);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_DATA_NASCIMENTO_INVALIDA;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }
}
