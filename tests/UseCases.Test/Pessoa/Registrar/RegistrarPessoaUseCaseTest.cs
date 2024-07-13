namespace UseCases.Test.Pessoa.Registrar;

public class RegistrarPessoaUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase();

        var resultado = await useCase.Executar(requisicao);

        resultado.Should().NotBeNull();
        resultado.Cpf.Should().Be(requisicao.Cpf);
        resultado.Cnpj.Should().Be(requisicao.Cnpj);
        resultado.Nome.Should().Be(requisicao.Nome);
        resultado.NomeFantasia.Should().Be(requisicao.NomeFantasia);
        resultado.Email.Should().Be(requisicao.Email);
        resultado.Nascimento.Should().Be(requisicao.Nascimento);
    }

    [Fact]
    public async Task CpfExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase(requisicao.Cpf);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CPF_JA_REGISTRADO));
    }

    [Fact]
    public async Task CnpjExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase(string.Empty, requisicao.Cnpj);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));
    }

    [Fact]
    public async Task CadastroExistente_DeveRetornarErro()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase(email: requisicao.Cadastro.Email);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));
    }

    private static RegistrarPessoaUseCase CriarUseCase(string? cpf = null, string? cnpj = null, string? email = null)
    {
        var repositorioWritePessoa = PessoaWriteOnlyRepositorioBuilder.Build();
        var repositorioReadPessoa = new PessoaReadOnlyRepositorioBuilder();
        var repositorioWriteCadastro = CadastroWriteOnlyRepositorioBuilder.Build();
        var repositorioReadCadastro = new CadastroReadOnlyRepositorioBuilder();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
        var viaCep = ViaCepBuilder.Build();

        if (string.IsNullOrWhiteSpace(cpf) == false)
            repositorioReadPessoa.RecuperarPessoaExistentePorCpf(cpf);

        else if (string.IsNullOrWhiteSpace(cnpj) == false)
            repositorioReadPessoa.RecuperarPessoaExistentePorCnpj(cnpj);

        else if (string.IsNullOrWhiteSpace(email) == false)
            repositorioReadCadastro.RecuperarCadastroExistentePorEmail(email);

        return new RegistrarPessoaUseCase(repositorioWriteCadastro, repositorioReadCadastro.Build(), repositorioReadPessoa.Build(), repositorioWritePessoa, viaCep, unidadeDeTrabalho);
    }
}
