namespace SistemaDeCadastro.Application.UseCases.Cadastro.Atualizar;

    public class AtualizarCadastroValidator : AbstractValidator<RequisicaoCadastroJson>
    {
        public AtualizarCadastroValidator() => RuleFor(x => x).SetValidator(new CadastroValidator());
    }
