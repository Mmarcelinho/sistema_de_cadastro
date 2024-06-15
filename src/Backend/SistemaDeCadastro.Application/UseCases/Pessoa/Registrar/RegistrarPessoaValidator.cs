namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaValidator : AbstractValidator<RequisicaoPessoaJson>
{
    public RegistrarPessoaValidator() => RuleFor(x => x).SetValidator(new PessoaValidator());
}
