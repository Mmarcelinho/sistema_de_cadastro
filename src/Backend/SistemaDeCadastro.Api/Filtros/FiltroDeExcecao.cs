namespace SistemaDeCadastro.Api.Filtros;

public class FiltroDeExcecao : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SistemaDeCadastroException)
            TratarSistemaDeCadastroException(context);

        else
            LancarErroDesconhecido(context);
    }

    private static void TratarSistemaDeCadastroException(ExceptionContext context)
    {
        var cadastroException = (SistemaDeCadastroException)context.Exception;
        var respostaDeErro = new RespostaErroJson(cadastroException.RecuperarErros());

        context.HttpContext.Response.StatusCode = cadastroException.StatusCode;

        context.Result = new ObjectResult(respostaDeErro);
    }

    private static void LancarErroDesconhecido(ExceptionContext context)
    {
        var respostaDeErro = new RespostaErroJson(MensagensDeErro.ERRO_DESCONHECIDO);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(respostaDeErro);
    }
}
