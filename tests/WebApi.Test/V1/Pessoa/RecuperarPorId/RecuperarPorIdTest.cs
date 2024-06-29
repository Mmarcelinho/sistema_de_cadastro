namespace WebApi.Test.V1.Pessoa.RecuperarPorId;

    public class RecuperarPorIdTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "pessoa";

    private readonly SistemaDeCadastro.Domain.Entidades.Pessoa _pessoa;

    public RecuperarPorIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    =>
        _pessoa = webApplicationFactory.RecuperarPessoa;
    

    [Fact]
    public async Task Sucesso()
    {
        var resultado = await DoGet(requestUri: $"{METODO}/{_pessoa.Id}");

        resultado.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("id").GetInt64().Should().Be(_pessoa.Id);
    }
}