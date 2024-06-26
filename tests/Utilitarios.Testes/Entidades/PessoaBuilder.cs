namespace Utilitarios.Testes.Entidades;

public class PessoaBuilder
{
    public static List<Pessoa> Colecao(uint count = 2)
    {
        var listPessoas = new List<Pessoa>();
        var cadastro = CadastroBuilder.Instancia();

        if (count == 0)
            count = 1;

        var pessoaId = 1;

        for (int i = 0; i < count; i++)
        {
            var pessoa = Instancia(cadastro);
            pessoa.Id = pessoaId++;

            listPessoas.Add(pessoa);
        }

        return listPessoas;
    }
    public static Pessoa Instancia(Cadastro cadastro)
    {
        var faker = new Faker();

        var endereco = new Endereco
        (
            faker.Address.ZipCode(),
            faker.Address.StreetName(),
            faker.Random.Number(1, 1000).ToString(),
            faker.Address.City(),
            faker.Address.SecondaryAddress(),
            faker.Lorem.Word(),
            faker.Address.StateAbbr(),
            faker.Address.City(),
            faker.Random.Number(100000, 999999).ToString()
        );

        var domicilio = new Domicilio
        {
            Tipo = faker.PickRandom<DomicilioTipo>(),
            Endereco = endereco
        };

        long numeroTelefone = 012345678;

        return new Faker<Pessoa>()
            .RuleFor(p => p.Id, _ => 1)
            .RuleFor(p => p.Cpf, faker => faker.Person.Cpf())
            .RuleFor(p => p.Cnpj, faker => faker.Company.Cnpj())
            .RuleFor(p => p.Nome, faker => faker.Person.FullName)
            .RuleFor(p => p.NomeFantasia, faker => faker.Company.CompanyName())
            .RuleFor(p => p.Email, faker => faker.Person.Email)
            .RuleFor(p => p.Nascimento, faker => faker.Person.DateOfBirth)
            .RuleFor(p => p.Token, faker => faker.Random.Int(1000, 9999))
            .RuleFor(p => p.Domicilios, _ => new List<Domicilio> { domicilio })
            .RuleFor(p => p.Telefone, faker => new Telefone
            (
                numeroTelefone,
                faker.Random.Bool(),
                faker.Random.Bool(),
                faker.Random.Bool()
            ))
            .RuleFor(p => p.Cadastro, _ => cadastro)
            .RuleFor(p => p.CadastroId, (p, _) => cadastro.Id);
    }
}