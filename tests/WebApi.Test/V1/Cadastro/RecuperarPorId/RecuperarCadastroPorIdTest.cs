namespace WebApi.Test.V1.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdTest(CustomWebApplicationFactory webApplicationFactory) : SistemaDeCadastroClassFixture(webApplicationFactory)
{
    private const string METODO = "cadastro";

    private readonly SistemaDeCadastro.Domain.Entidades.Cadastro _cadastro = webApplicationFactory.Cadastro;

    [Fact]
    public async Task Sucesso()
    {
        var resultado = await DoGet(requestUri: $"{METODO}/{_cadastro.Id}");

        resultado.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("id").GetInt64().Should().Be(_cadastro.Id);
    }
}