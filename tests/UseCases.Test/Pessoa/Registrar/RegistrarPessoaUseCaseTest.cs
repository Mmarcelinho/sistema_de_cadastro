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
    public async Task DeveRetornarErroQuandoCpfJaExistente()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase(requisicao.Cpf);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CPF_JA_REGISTRADO));
    }

    [Fact]
    public async Task DeveRetornarErroQuandoCnpjJaExistente()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

        var useCase = CriarUseCase(string.Empty, requisicao.Cnpj);

        Func<Task> acao = async () => await useCase.Executar(requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));
    }

    private static RegistrarPessoaUseCase CriarUseCase(string? cpf = null, string? cnpj = null)
    {
        var repositorioWrite = PessoaWriteOnlyRepositorioBuilder.Build();
        var repositorioRead = new PessoaReadOnlyRepositorioBuilder();
        var repositorioCadastro = CadastroWriteOnlyRepositorioBuilder.Build();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
        var viaCep = ViaCepBuilder.Build();

        if (string.IsNullOrWhiteSpace(cpf) == false)
            repositorioRead.RecuperarPessoaExistentePorCpf(cpf);

        else if (string.IsNullOrWhiteSpace(cnpj) == false)
            repositorioRead.RecuperarPessoaExistentePorCnpj(cnpj);

        return new RegistrarPessoaUseCase(repositorioCadastro, repositorioRead.Build(), repositorioWrite, viaCep, unidadeDeTrabalho);
    }
}
