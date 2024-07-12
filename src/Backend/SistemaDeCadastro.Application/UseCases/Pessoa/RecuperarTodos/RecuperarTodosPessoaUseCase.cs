namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarTodos;

public class RecuperarTodosPessoaUseCase : IRecuperarTodosPessoaUseCase
{
    private readonly IPessoaReadOnlyRepositorio _repositorio;

    public RecuperarTodosPessoaUseCase(IPessoaReadOnlyRepositorio repositorio)
    =>
        _repositorio = repositorio;
    
    public async Task<RespostaPessoasJson> Executar()
    {
        var pessoas = await _repositorio.RecuperarTodos();

        return new RespostaPessoasJson
        {
            Pessoas = pessoas.Select(pessoa => pessoa.ConverterParaResposta()).ToList()
        };
    }  
}
