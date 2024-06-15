namespace SistemaDeCadastro.Api.Controllers;

    public class PessoaController : SistemaDeCadastroController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaPessoaJson),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar([FromServices] IRegistrarPessoaUseCase useCase, [FromBody] RequisicaoPessoaJson requisicao)
        {
            var resposta = await useCase.Executar(requisicao);

            return Created(string.Empty, resposta);
        }
    }
