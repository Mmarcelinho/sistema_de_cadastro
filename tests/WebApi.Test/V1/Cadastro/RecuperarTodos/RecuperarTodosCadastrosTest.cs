namespace WebApi.Test.V1.Cadastro.RecuperarTodos;

public class RecuperarTodosCadastrosTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "cadastro";

    public RecuperarTodosCadastrosTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    { }

    [Fact]
    public async Task Sucesso()
    {
        var resultado = await DoGet(requestUri: METODO);

        resultado.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("cadastros").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
