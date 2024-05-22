namespace SistemaDeCadastro.Domain.ValueObjects;

public class Identificacao : ValueObject
{
    public int Empresa { get; }

    public string Identificador { get; }

    public IdentificacaoTipo Tipo { get; }

    public Identificacao(int empresa, string identificador, IdentificacaoTipo tipo)
    {
        Empresa = empresa;
        Identificador = identificador;
        Tipo = tipo;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Empresa;
        yield return Identificador;
        yield return Tipo;
    }
}
