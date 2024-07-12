namespace WebApi.Test.V1.Pessoa.Atualizar;

public class AtualizarPessoaTest : SistemaDeCadastroClassFixture
{
    private const string METODO = "pessoa";

    private readonly SistemaDeCadastro.Domain.Entidades.Pessoa _pessoa;

    public AtualizarPessoaTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    =>
        _pessoa = webApplicationFactory.Pessoa;

    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicao);

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

    [Fact]
    public async Task Erro_Cpf_EmBranco()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoCpfVazio = requisicao with { Cpf = string.Empty };

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicaoCpfVazio);

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

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicaoNomeVazio);

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

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicaoNomeFantasiaVazio);

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

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicaoNascimentoInvalido);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_DATA_NASCIMENTO_INVALIDA;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }

    [Fact]
    public async Task Erro_Cadastro_EmBranco()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var requisicaoCadastroVazio = requisicao with { Cadastro = null };

        var resultado = await DoPut(requestUri: $"{METODO}/{_pessoa.Id}", request: requisicaoCadastroVazio);

        resultado.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await resultado.Content.ReadAsStreamAsync();

        var resposta = await JsonDocument.ParseAsync(body);

        var erros = resposta.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = PessoaMensagensDeErro.PESSOA_CADASTRO_EMBRANCO;

        erros.Should().HaveCount(1).And.Contain(erro => erro.GetString()!.Equals(mensagemEsperada));
    }
}
