using System.Net;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public class NaoEncontradoException(string mensagem) : SistemaDeCadastroException(mensagem)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> RecuperarErros()
    => [Message];
}
