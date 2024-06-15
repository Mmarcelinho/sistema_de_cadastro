namespace SistemaDeCadastro.Domain.ValueObjects;

public class Telefone : ValueObject
{
    public long Numero { get; }

    public bool Celular { get; }

    public bool Whatsapp { get; }

    public bool Telegram { get; }

    public Telefone(long numero, bool celular, bool whatsapp, bool telegram)
    {
        Numero = numero;
        Celular = celular;
        Whatsapp = whatsapp;
        Telegram = telegram;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Numero;
        yield return Celular;
        yield return Whatsapp;
        yield return Telegram;
    }
}
