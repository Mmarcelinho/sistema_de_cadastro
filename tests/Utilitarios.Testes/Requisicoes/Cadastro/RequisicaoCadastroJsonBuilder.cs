namespace Utilitarios.Testes.Requisicoes.Cadastro;

public class RequisicaoCadastroJsonBuilder
{
    public static RequisicaoCadastroJson Build()
    {
        var faker = new Faker();

        return new Faker<RequisicaoCadastroJson>()
            .RuleFor(r => r.Email, f => f.Internet.Email())
            .RuleFor(r => r.NomeFantasia, f => f.Company.CompanyName())
            .RuleFor(r => r.SobrenomeSocial, f => f.Name.LastName())
            .RuleFor(r => r.Empresa, f => f.Random.Bool())
            .RuleFor(r => r.Credencial, f => new RequisicaoCredencialJson(
                f.Random.Bool(),
                f.Date.Future().ToString("yyyy-MM-dd"),
                f.Internet.Password()
            ))
            .RuleFor(r => r.Inscrito, f => new RequisicaoInscritoJson(
                f.Random.Bool(),
                f.Random.Bool(),
                f.Internet.Password()
            ))
            .RuleFor(r => r.Parceiro, f => new RequisicaoParceiroJson(
                f.Random.Bool(),
                f.Random.Bool(),
                f.Random.Bool(),
                f.Random.Bool()
            ))
            .RuleFor(r => r.Documento, f => new RequisicaoDocumentoJson(
                f.Random.Replace("###########"),
                f.Company.CompanyName(),
                f.Address.StateAbbr(),
                f.Date.Future()
            ))
            .RuleFor(r => r.Identificador, f => new RequisicaoIdentificacaoJson(
                f.Random.Int(1, 10),
                f.Random.AlphaNumeric(10),
                f.PickRandom<SistemaDeCadastro.Communication.Enum.IdentificacaoTipo>()
            ));
    }
}