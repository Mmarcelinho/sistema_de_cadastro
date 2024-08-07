namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarTodos;

public class RecuperarTodosCadastroUseCase(ICadastroReadOnlyRepositorio repositorio) : IRecuperarTodosCadastroUseCase
{
    public async Task<RespostaCadastrosJson> Executar()
    {
        var cadastros = await repositorio.RecuperarTodos();

        return new RespostaCadastrosJson
        {
            Cadastros = cadastros.Select(cadastro => cadastro.ConverterParaResposta()).ToList()
        };
    }
}
