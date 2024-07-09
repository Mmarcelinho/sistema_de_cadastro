using SistemaDeCadastro.Application.Mappings;

namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarTodos;

public class RecuperarTodosCadastroUseCase : IRecuperarTodosCadastroUseCase
{
    private readonly ICadastroReadOnlyRepositorio _repositorio;

    public RecuperarTodosCadastroUseCase(ICadastroReadOnlyRepositorio repositorio)
    =>
        _repositorio = repositorio;

    public async Task<RespostaCadastrosJson> Executar()
    {
        var cadastros = await _repositorio.RecuperarTodos();

        return new RespostaCadastrosJson
        {
            Cadastros = cadastros.Select(cadastro => cadastro.ConverterParaResposta()).ToList()
        };
    }
}
