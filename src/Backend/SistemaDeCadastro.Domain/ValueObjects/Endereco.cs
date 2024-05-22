namespace SistemaDeCadastro.Domain.ValueObjects;

public class Endereco : ValueObject
{
    public string Cep { get; }

    public string Logradouro { get; }

    public string Numero { get; }

    public string Bairro { get; }

    public string Complemento { get; }

    public string PontoReferencia { get; }

    public string UF { get; }

    public string Cidade { get; }

    public int Ibge { get; }

    public Endereco(string cep, string logradouro, string numero, string bairro, string complemento, string pontoReferencia, string uF, string cidade, int ibge)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Complemento = complemento;
        PontoReferencia = pontoReferencia;
        UF = uF;
        Cidade = cidade;
        Ibge = ibge;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Cep;

        yield return Logradouro;

        yield return Numero;

        yield return Bairro;

        yield return Complemento;

        yield return PontoReferencia;

        yield return UF;

        yield return Cidade;

        yield return Ibge;
    }
}
