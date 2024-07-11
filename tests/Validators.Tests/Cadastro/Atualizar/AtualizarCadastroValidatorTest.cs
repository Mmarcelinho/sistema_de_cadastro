using SistemaDeCadastro.Application.UseCases.Cadastro.Atualizar;

namespace Validators.Tests.Cadastro.Atualizar;

public class AtualizarCadastroValidatorTest
{
    [Fact]
    public void Sucesso()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void EmailEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Email = string.Empty };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();

        var mensagensDeErro = new List<string>
        {
            CadastroMensagensDeErro.CADASTRO_EMAIL_EMBRANCO,
            CadastroMensagensDeErro.CADASTRO_EMAIL_INVALIDO
        };

        foreach (var mensagem in mensagensDeErro)
        {
            resultado.Errors.Should().Contain(e => e.ErrorMessage == mensagem);
        }
    }

    [Fact]
    public void EmailInvalido_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Email = "emailinvalido" };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_EMAIL_INVALIDO));
    }

    [Fact]
    public void NomeFantasiaEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { NomeFantasia = string.Empty };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_NOME_FANTASIA_EMBRANCO));
    }

    [Fact]
    public void CredencialSenhaEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Credencial = new RequisicaoCredencialJson(true, "2024-12-31", string.Empty) };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_CREDENCIAL_SENHA_EMBRANCO));
    }

    [Fact]
    public void DocumentoNumeroEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Documento = new RequisicaoDocumentoJson(string.Empty, "orgaoEmissor", "estadoEmissor", DateTime.Now) };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_NUMERO_EMBRANCO));
    }

    [Fact]
    public void DocumentoOrgaoEmissorEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Documento = new RequisicaoDocumentoJson("numero", string.Empty, "estadoEmissor", DateTime.Now) };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_ORGAO_EMISSOR_EMBRANCO));
    }

    [Fact]
    public void DocumentoDataValidadeInvalida_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Documento = new RequisicaoDocumentoJson("numero", "orgaoEmissor", "estadoEmissor", DateTime.MinValue) };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_DATA_VALIDADE_INVALIDA));
    }

    [Fact]
    public void IdentificadorEmBranco_DeveRetornarErro()
    {
        var validator = new AtualizarCadastroValidator();

        var requisicao = RequisicaoCadastroJsonBuilder.Instancia() with { Identificador = null! };

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(CadastroMensagensDeErro.CADASTRO_IDENTIFICADOR_EMBRANCO));
    }
}
