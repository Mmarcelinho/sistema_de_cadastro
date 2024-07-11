namespace CommonTestUtilities.Requisicoes.Cadastro;

public class RequisicaoCadastroJsonBuilder
{
    public static RequisicaoCadastroJson Instancia()
    {
        var faker = new Faker();

        return new RequisicaoCadastroJson(
            faker.Internet.Email(),
            faker.Company.CompanyName(),
            faker.Name.LastName(),
            faker.Random.Bool(),
            new RequisicaoCredencialJson(
                faker.Random.Bool(),
                faker.Date.Future().ToString(),
                faker.Internet.Password()
            ),
            new RequisicaoInscritoJson(
                faker.Random.Bool(),
                faker.Random.Bool(),
                faker.Internet.Password()
            ),
            new RequisicaoParceiroJson(
                faker.Random.Bool(),
                faker.Random.Bool(),
                faker.Random.Bool(),
                faker.Random.Bool()
            ),
            new RequisicaoDocumentoJson(
                faker.Random.ReplaceNumbers("###########"),
                faker.Company.CompanyName(),
                faker.Address.StateAbbr(),
                faker.Date.Future()
            ),
            new RequisicaoIdentificacaoJson(
                faker.Random.Int(1, 10),
                faker.Random.AlphaNumeric(10),
                faker.PickRandom<SistemaDeCadastro.Communication.Enum.IdentificacaoTipo>()
            )
        );
    }
}