namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarTodos;

public class RecuperarTodosPessoaUseCase(IPessoaReadOnlyRepositorio repositorio) : IRecuperarTodosPessoaUseCase
{
    public async Task<RespostaPessoasJson> Executar()
    {
        var pessoas = await repositorio.RecuperarTodos();

        return new RespostaPessoasJson
        {
            Pessoas = pessoas.Select(pessoa => pessoa.ConverterParaResposta()).ToList()
        };
    }  
}
