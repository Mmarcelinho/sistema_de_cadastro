namespace SistemaDeCadastro.Api.Controllers;

public class PessoaController : SistemaDeCadastroController
{
    [HttpGet]
    [ProducesResponseType(typeof(RespostaPessoaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarTodos([FromServices] IRecuperarTodosPessoaUseCase useCase)
    {
        var resposta = await useCase.Executar();

        if (resposta.Pessoas.Count == 0)
            return NoContent();

        return Ok(resposta);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(RespostaPessoaJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperarPorId([FromServices] IRecuperarPessoaPorIdUseCase useCase, [FromRoute] long id)
    {
        var resposta = await useCase.Executar(id);

        return Ok(resposta);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RespostaPessoaJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromServices] IRegistrarPessoaUseCase useCase, [FromBody] RequisicaoPessoaJson requisicao)
    {
        var resposta = await useCase.Executar(requisicao);

        return Created(string.Empty, resposta);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar([FromServices] IDeletarPessoaPorIdUseCase useCase, [FromRoute] long id)
    {
        await useCase.Executar(id);

        return NoContent();
    }
}
