namespace SistemaDeCadastro.Application.UseCases.Pessoa.Atualizar;

    public class AtualizarPessoaValidator : AbstractValidator<RequisicaoPessoaJson>
    {
        public AtualizarPessoaValidator() => RuleFor(x => x).SetValidator(new PessoaValidator());
    }
