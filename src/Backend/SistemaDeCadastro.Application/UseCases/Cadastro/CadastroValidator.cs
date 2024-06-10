namespace SistemaDeCadastro.Application.UseCases.Cadastro;

public class CadastroValidator : AbstractValidator<RequisicaoCadastroJson>
{
    public CadastroValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_EMAIL_EMBRANCO)
        .EmailAddress().WithMessage(CadastroMensagensDeErro.CADASTRO_EMAIL_INVALIDO);

        RuleFor(x => x.NomeFantasia)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_NOME_FANTASIA_EMBRANCO);

        RuleFor(x => x.Credencial.Senha)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_CREDENCIAL_SENHA_EMBRANCO);

        RuleFor(x => x.Documento.Numero)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_NUMERO_EMBRANCO);

        RuleFor(x => x.Documento.OrgaoEmissor)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_ORGAO_EMISSOR_EMBRANCO);

        RuleFor(x => x.Documento.DataValidade)
        .GreaterThan(DateTime.MinValue).WithMessage(CadastroMensagensDeErro.CADASTRO_DOCUMENTO_DATA_VALIDADE_INVALIDA);

        RuleFor(x => x.Identificador)
        .NotEmpty().WithMessage(CadastroMensagensDeErro.CADASTRO_IDENTIFICADOR_EMBRANCO);

        RuleFor(x => x.Empresa)
        .NotNull().WithMessage(CadastroMensagensDeErro.CADASTRO_EMPRESA_INVALIDA);
    }
}
