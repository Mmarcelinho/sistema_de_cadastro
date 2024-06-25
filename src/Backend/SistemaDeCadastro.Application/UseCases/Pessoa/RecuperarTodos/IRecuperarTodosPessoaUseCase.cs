namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarTodos;

    public interface IRecuperarTodosPessoaUseCase
    {
        Task<IEnumerable<RespostaPessoaJson>> Executar();
    }
