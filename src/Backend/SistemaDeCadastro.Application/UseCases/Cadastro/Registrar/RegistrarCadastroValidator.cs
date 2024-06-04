namespace SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;

    public class RegistrarCadastroValidator : AbstractValidator<RequisicaoCadastroJson>
    {
        public RegistrarCadastroValidator() => RuleFor(x => x).SetValidator(new CadastroValidator());
    }
