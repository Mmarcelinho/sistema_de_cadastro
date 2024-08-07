using System.Net;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public class ErrosDeValidacaoException(List<string> mensagensDeErro) : SistemaDeCadastroException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> RecuperarErros() => mensagensDeErro;
}
