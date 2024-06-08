namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public abstract class SistemaDeCadastroException : SystemException
{
    protected SistemaDeCadastroException(string mensagem) : base(mensagem) { }

    public abstract int StatusCode { get; }

    public abstract List<string> RecuperarErros();
}
