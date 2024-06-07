namespace SistemaDeCadastro.Api.Controllers;

    public class CadastroController : SistemaDeCadastroController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaCadastroJson),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar([FromServices] IRegistrarCadastroUseCase useCase, [FromBody] RequisicaoCadastroJson requisicao)
        {
            var resposta = await useCase.Executar(requisicao);

            return Created(string.Empty, resposta);
        }
    }
