namespace WebApi.Test.V1.Pessoa.RecuperarTodos;

public class RecuperarTodosTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "pessoa";

    public RecuperarTodosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    { }

    [Fact]
    public async Task Sucesso()
    {
        var resultado = await DoGet(requestUri: METODO);

        resultado.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("pessoas").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}