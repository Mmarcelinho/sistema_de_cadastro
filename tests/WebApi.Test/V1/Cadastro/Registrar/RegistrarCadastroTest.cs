using System.Net;
using System.Text.Json;
using FluentAssertions;
using Utilitarios.Testes.Requisicoes.Cadastro;

namespace WebApi.Test.V1.Cadastro.Registrar;

public class RegistrarCadastroTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "cadastro";
    public RegistrarCadastroTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    { }

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoCadastroJsonBuilder.Build();

        var resultado = await DoPost(requestUri: METODO, request: requisicao);

        resultado.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        resposta.RootElement.GetProperty("id").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
