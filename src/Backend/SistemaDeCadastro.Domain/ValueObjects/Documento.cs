namespace SistemaDeCadastro.Domain.ValueObjects;

public class Documento : ValueObject
{
    public string Numero { get; }

    public string OrgaoEmissor { get; }

    public string EstadoEmissor { get; }

    public DateTime DataValidade { get; }

    public Documento(string numero, string orgaoEmissor, string estadoEmissor, DateTime dataValidade)
    {
        Numero = numero;
        OrgaoEmissor = orgaoEmissor;
        EstadoEmissor = estadoEmissor;
        DataValidade = dataValidade;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Numero;
        yield return OrgaoEmissor;
        yield return EstadoEmissor;
        yield return DataValidade;
    }
}
