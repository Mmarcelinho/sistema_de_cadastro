namespace Utilitarios.Testes.Requisicoes.Pessoa;

public class RequisicaoPessoaJsonBuilder
{
    public static RequisicaoPessoaJson Build()
    {
        var faker = new Faker();

        return new RequisicaoPessoaJson(
            faker.Random.ReplaceNumbers("###########"),
            faker.Random.ReplaceNumbers("##############"),
            faker.Person.FullName,
            faker.Company.CompanyName(),
            faker.Internet.Email(),
            faker.Date.Past(30, DateTime.Now.AddYears(-18)),
            faker.Random.Int(1000, 9999),
            [
                new RequisicaoDomicilioJson(
                    faker.PickRandom<SistemaDeCadastro.Communication.Enum.DomicilioTipo>(),
                    new RequisicaoEnderecoJson(
                        "16016020",
                        faker.Address.BuildingNumber(),
                        faker.Address.SecondaryAddress(),
                        faker.Lorem.Sentence()
                    )
                )
            ],
            new RequisicaoTelefoneJson(
                long.Parse(faker.Phone.PhoneNumber("###########")),
                faker.Random.Bool(),
                faker.Random.Bool(),
                faker.Random.Bool()
            ),
            RequisicaoCadastroJsonBuilder.Build()
        );
    }
}