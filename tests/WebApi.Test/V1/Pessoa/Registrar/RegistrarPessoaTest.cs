namespace WebApi.Test.V1.Pessoa.Registrar;

public class RegistrarPessoaTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "pessoa";

    public RegistrarPessoaTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    { }

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Build();

        var resultado = await DoPost(requestUri: METODO, request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
