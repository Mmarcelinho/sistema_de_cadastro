namespace Utilitarios.Testes.Entidades;

public class CadastroBuilder
{
    public static Cadastro Build()
    {
        return new Faker<Cadastro>()
            .RuleFor(c => c.Id, _ => 1)
            .RuleFor(c => c.Email, faker => faker.Internet.Email())
            .RuleFor(c => c.NomeFantasia, faker => faker.Company.CompanyName())
            .RuleFor(c => c.SobrenomeSocial, faker => faker.Name.LastName())
            .RuleFor(c => c.Empresa, faker => faker.Random.Bool())
            .RuleFor(c => c.Credencial, faker => new Credencial
            {
                Bloqueada = faker.Random.Bool(),
                Expirada = faker.Date.Future().ToString(),
                Senha = faker.Internet.Password()
            })
            .RuleFor(c => c.Inscrito, faker => new Inscrito
            {
                Assinante = faker.Random.Bool(),
                Associado = faker.Random.Bool(),
                Senha = faker.Internet.Password()
            })
            .RuleFor(c => c.Parceiro, faker => new Parceiro
            {
                Cliente = faker.Random.Bool(),
                Fornecedor = faker.Random.Bool(),
                Prestador = faker.Random.Bool(),
                Colaborador = faker.Random.Bool()
            })
            .RuleFor(c => c.Documento, faker => new Documento
            (
                faker.Random.ReplaceNumbers("###########"),
                faker.Company.CompanyName(),
                faker.Address.StateAbbr(),
                faker.Date.Future()
            ))
            .RuleFor(c => c.Identificador, faker => new Identificacao
            (
                faker.Random.Int(1, 10),
                faker.Random.AlphaNumeric(10),
                faker.PickRandom<IdentificacaoTipo>()
            ));
    }
}