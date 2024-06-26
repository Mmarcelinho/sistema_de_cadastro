
using System.Net;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public class NaoEncontradoException : SistemaDeCadastroException
{
    public NaoEncontradoException(string mensagem) : base(mensagem)
    { }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> RecuperarErros()
    => [Message];
}
