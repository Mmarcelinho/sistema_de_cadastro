namespace WebApi.Test.V1.Pessoa.RecuperarTodos;

public class RecuperarTodosPessoasTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "pessoa";

    public RecuperarTodosPessoasTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
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