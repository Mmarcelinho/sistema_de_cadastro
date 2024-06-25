namespace SistemaDeCadastro.Api.Controllers;

public class CadastroController : SistemaDeCadastroController
{
    [HttpGet]
    [ProducesResponseType(typeof(RespostaCadastroJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarTodos([FromServices] IRecuperarTodosCadastroUseCase useCase)
    {
        var resposta = await useCase.Executar();

        return Ok(resposta);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(RespostaCadastroJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromServices] IRegistrarCadastroUseCase useCase, [FromBody] RequisicaoCadastroJson requisicao)
    {
        var resposta = await useCase.Executar(requisicao);

        return Created(string.Empty, resposta);
    }
}
