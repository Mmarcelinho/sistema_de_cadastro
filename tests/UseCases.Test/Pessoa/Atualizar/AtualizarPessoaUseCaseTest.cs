namespace UseCases.Test.Pessoa.Atualizar;

public class AtualizarPessoaUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);
        var useCase = CriarUseCase(pessoa);

        var acao = async () => await useCase.Executar(pessoa.Id, requisicao);

        await acao.Should().NotThrowAsync();

        pessoa.Cpf.Should().Be(requisicao.Cpf);
        pessoa.Cnpj.Should().Be(requisicao.Cnpj);
        pessoa.Nome.Should().Be(requisicao.Nome);
        pessoa.NomeFantasia.Should().Be(requisicao.NomeFantasia);
        pessoa.Email.Should().Be(requisicao.Email);
        pessoa.Nascimento.Should().Be(requisicao.Nascimento);
    }

    [Fact]
    public async Task PessoaNaoEncontrado_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(pessoa);

        var acao = async () => await useCase.Executar(pessoaId: 100, requisicao);

        await acao.Should().ThrowAsync<NaoEncontradoException>()
        .Where(exception => exception.RecuperarErros().Count == 1 && exception.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO));
    }

    [Fact]
    public async Task CpfExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);
        var useCase = CriarUseCase(pessoa, requisicao.Cpf);

        Func<Task> acao = async () => await useCase.Executar(pessoa.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CPF_JA_REGISTRADO));
    }

    [Fact]
    public async Task CnpjExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);
        var useCase = CriarUseCase(pessoa, string.Empty, requisicao.Cnpj);

        Func<Task> acao = async () => await useCase.Executar(pessoa.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));
    }

    [Fact]
    public async Task CadastroExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);

        var useCase = CriarUseCase(pessoa, email: requisicao.Cadastro.Email);

        Func<Task> acao = async () => await useCase.Executar(pessoa.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));
    }

    private static AtualizarPessoaUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Pessoa? pessoa = null, string? cpf = null, string? cnpj = null, string? email = null)
    {
        var repositorioUpdatePessoa = new PessoaUpdateOnlyRepositorioBuilder().RecuperarPorId(pessoa).Instancia();
        var repositorioReadPessoa = new PessoaReadOnlyRepositorioBuilder();
        var repositorioReadCadastro = new CadastroReadOnlyRepositorioBuilder();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia();
        var cepService = CepServiceBuilder.Instancia();

        if (string.IsNullOrWhiteSpace(cpf) == false)
            repositorioReadPessoa.RecuperarPessoaExistentePorCpf(cpf);

        else if (string.IsNullOrWhiteSpace(cnpj) == false)
            repositorioReadPessoa.RecuperarPessoaExistentePorCnpj(cnpj);

        else if (string.IsNullOrWhiteSpace(email) == false)
            repositorioReadCadastro.RecuperarCadastroExistentePorEmail(email);

        return new AtualizarPessoaUseCase(repositorioUpdatePessoa, repositorioReadPessoa.Instancia(), repositorioReadCadastro.Instancia(), unidadeDeTrabalho, cepService);
    }
}
