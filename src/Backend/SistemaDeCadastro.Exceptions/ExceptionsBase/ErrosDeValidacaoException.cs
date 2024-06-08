using System.Net;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

public class ErrosDeValidacaoException : SistemaDeCadastroException
{
    private readonly List<string> _erros;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrosDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty) => _erros = mensagensDeErro;

    public override List<string> RecuperarErros() => _erros;
}
