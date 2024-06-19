namespace Utilitarios.Testes.Requisicoes.Pessoa;

public class RequisicaoPessoaJsonBuilder
{
    public static RequisicaoPessoaJson Build()
    {
        var faker = new Faker();

        return new Faker<RequisicaoPessoaJson>()
            .RuleFor(r => r.Cpf, f => f.Random.ReplaceNumbers("###########"))
            .RuleFor(r => r.Cnpj, f => f.Random.ReplaceNumbers("##############"))
            .RuleFor(r => r.Nome, f => f.Person.FullName)
            .RuleFor(r => r.NomeFantasia, f => f.Company.CompanyName())
            .RuleFor(r => r.Email, f => f.Internet.Email())
            .RuleFor(r => r.Nascimento, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
            .RuleFor(r => r.Token, f => f.Random.Int(1000, 9999))
            .RuleFor(r => r.Domicilios, f => new List<RequisicaoDomicilioJson>
            {
                    new RequisicaoDomicilioJson(
                        f.PickRandom<SistemaDeCadastro.Communication.Enum.DomicilioTipo>(),
                        new RequisicaoEnderecoJson(
                            f.Address.ZipCode("########"),
                            f.Address.BuildingNumber(),
                            f.Address.SecondaryAddress(),
                            f.Lorem.Sentence()
                        )
                    )
            })
            .RuleFor(r => r.Telefone, f => new RequisicaoTelefoneJson(
                long.Parse(f.Phone.PhoneNumber("###########")),
                f.Random.Bool(),
                f.Random.Bool(),
                f.Random.Bool()
            ))
            .RuleFor(r => r.Cadastro, _ => RequisicaoCadastroJsonBuilder.Build());
    }
}
