namespace SistemaDeCadastro.Application.UseCases.Pessoa;

    public class PessoaValidator : AbstractValidator<RequisicaoPessoaJson>
    {
        public PessoaValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_CPF_EMBRANCO)
                .Matches(@"^\d{11}$").WithMessage(PessoaMensagensDeErro.PESSOA_CPF_INVALIDO);

            RuleFor(x => x.Cnpj)
                .Matches(@"^\d{14}$").When(x => !string.IsNullOrEmpty(x.Cnpj))
                .WithMessage(PessoaMensagensDeErro.PESSOA_CNPJ_INVALIDO);

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_NOME_EMBRANCO);

            RuleFor(x => x.NomeFantasia)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_NOME_FANTASIA_EMBRANCO);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_EMAIL_EMBRANCO)
                .EmailAddress().WithMessage(PessoaMensagensDeErro.PESSOA_EMAIL_INVALIDO);

            RuleFor(x => x.Nascimento)
                .GreaterThan(DateTime.MinValue).WithMessage(PessoaMensagensDeErro.PESSOA_DATA_NASCIMENTO_INVALIDA);

            RuleFor(x => x.Token)
                .GreaterThan(0).WithMessage(PessoaMensagensDeErro.PESSOA_TOKEN_INVALIDO);

            RuleFor(x => x.Domicilios)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_DOMICILIOS_EMBRANCO)
                .ForEach(domicilio =>
                {
                    domicilio.ChildRules(d =>
                    {
                        d.RuleFor(d => d.Endereco.Cep)
                            .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_DOMICILIO_CEP_EMBRANCO);
                    });
                });

            RuleFor(x => x.Telefone.Numero)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_TELEFONE_EMBRANCO);

            RuleFor(x => x.Cadastro)
                .NotEmpty().WithMessage(PessoaMensagensDeErro.PESSOA_CADASTRO_EMBRANCO)
                .SetValidator(new CadastroValidator());
        }
    }
