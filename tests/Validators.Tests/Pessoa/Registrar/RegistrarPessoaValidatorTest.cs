namespace Validators.Tests.Pessoa.Registrar;

    public class RegistrarPessoaValidatorTest
    {
        [Fact]
        public void Sucesso()
        {
            var validator = new RegistrarPessoaValidator();

            var requisicao = RequisicaoPessoaJsonBuilder.Instancia();

            var resultado = validator.Validate(requisicao);

            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
    public void CpfEmBranco_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { Cpf = string.Empty };

        var resultado = validator.Validate(requisicao);

        var mensagensDeErro = new List<string>
        {
            PessoaMensagensDeErro.PESSOA_CPF_EMBRANCO,
            PessoaMensagensDeErro.PESSOA_CPF_INVALIDO
        };

        foreach (var mensagem in mensagensDeErro)
        {
            resultado.Errors.Should().Contain(e => e.ErrorMessage == mensagem);
        }
    }

    [Fact]
    public void NomeEmBranco_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { Nome = string.Empty };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(PessoaMensagensDeErro.PESSOA_NOME_EMBRANCO));
    }

    [Fact]
    public void NomeFantasiaEmBranco_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { NomeFantasia = string.Empty };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(PessoaMensagensDeErro.PESSOA_NOME_FANTASIA_EMBRANCO));
    }

    [Fact]
    public void EmailEmBranco_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { Email = string.Empty };

        var resultado = validator.Validate(requisicao);

        var mensagensDeErro = new List<string>
        {
            PessoaMensagensDeErro.PESSOA_EMAIL_EMBRANCO,
            PessoaMensagensDeErro.PESSOA_EMAIL_INVALIDO
        };

        foreach (var mensagem in mensagensDeErro)
        {
            resultado.Errors.Should().Contain(e => e.ErrorMessage == mensagem);
        }
    }

    [Fact]
    public void NascimentoInvalido_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { Nascimento = DateTime.MinValue };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(PessoaMensagensDeErro.PESSOA_DATA_NASCIMENTO_INVALIDA));
    }

    [Fact]
    public void TokenInvalido_DeveRetornarErro()
    {
        var validator = new RegistrarPessoaValidator();

        var requisicao = RequisicaoPessoaJsonBuilder.Instancia() with { Token = 0 };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(PessoaMensagensDeErro.PESSOA_TOKEN_INVALIDO));
    }
}
    
