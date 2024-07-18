namespace WebApi.Test.V1.Cadastro.Deletar;

public class DeletarCadastroTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "cadastro";

    private readonly SistemaDeCadastro.Domain.Entidades.Cadastro _cadastro;

    public DeletarCadastroTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    =>
        _cadastro = webApplicationFactory.Cadastro;

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var resultado = await DoDelete(requestUri: $"{METODO}/{_cadastro.Id}");

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
}