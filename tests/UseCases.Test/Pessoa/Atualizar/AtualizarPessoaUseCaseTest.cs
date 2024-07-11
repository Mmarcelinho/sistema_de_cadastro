using SistemaDeCadastro.Application.UseCases.Pessoa.Atualizar;

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
    public async Task DeveRetornarErroQuandoCpfJaExistente()
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
    public async Task DeveRetornarErroQuandoCnpjJaExistente()
    {
        var requisicao = RequisicaoPessoaJsonBuilder.Instancia();
        var cadastro = CadastroBuilder.Instancia();
        var pessoa = PessoaBuilder.Instancia(cadastro);
        var useCase = CriarUseCase(pessoa, string.Empty, requisicao.Cnpj);

        Func<Task> acao = async () => await useCase.Executar(pessoa.Id, requisicao);

        var resultado = await acao.Should().ThrowAsync<ErrosDeValidacaoException>();

        resultado.Where(ex => ex.RecuperarErros().Count == 1 && ex.RecuperarErros().Contains(PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));
    }

    private AtualizarPessoaUseCase CriarUseCase(SistemaDeCadastro.Domain.Entidades.Pessoa? pessoa = null, string? cpf = null, string? cnpj = null)
    {
        var repositorioUpdate = new PessoaUpdateOnlyRepositorioBuilder().RecuperarPorId(pessoa).Build();
        var repositorioRead = new PessoaReadOnlyRepositorioBuilder();
        var repositorioCadastro = CadastroWriteOnlyRepositorioBuilder.Build();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Build();
        var viaCep = ViaCepBuilder.Build();

        if (string.IsNullOrWhiteSpace(cpf) == false)
            repositorioRead.RecuperarPessoaExistentePorCpf(cpf);

        else if (string.IsNullOrWhiteSpace(cnpj) == false)
            repositorioRead.RecuperarPessoaExistentePorCnpj(cnpj);

        return new AtualizarPessoaUseCase(repositorioUpdate, repositorioRead.Build(), unidadeDeTrabalho, viaCep);
    }
}
