namespace WebApi.Test.V1.Pessoa.Deletar;

public class DeletarPessoaTest(CustomWebApplicationFactory webApplicationFactory) : SistemaDeCadastroClassFixture(webApplicationFactory)
{
    private const string METODO = "pessoa";

    private readonly SistemaDeCadastro.Domain.Entidades.Pessoa _pessoa = webApplicationFactory.Pessoa;

    [Fact]
    public async Task Sucesso()
    {
        var resultado = await DoDelete(requestUri: $"{METODO}/{_pessoa.Id}");

        resultado.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Erro_Pessoa_NaoEncontrado()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var resultado = await DoPut(requestUri: $"{METODO}/{100}", request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }
}
