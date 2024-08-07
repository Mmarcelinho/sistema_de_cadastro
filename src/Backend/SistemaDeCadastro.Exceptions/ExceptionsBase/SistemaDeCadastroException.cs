namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public abstract class SistemaDeCadastroException(string mensagem) : SystemException(mensagem)
{
    public abstract int StatusCode { get; }

    public abstract List<string> RecuperarErros();
}
