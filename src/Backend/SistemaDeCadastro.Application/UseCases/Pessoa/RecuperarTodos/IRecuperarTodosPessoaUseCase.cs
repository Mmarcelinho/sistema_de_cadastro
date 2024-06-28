namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarTodos;

    public interface IRecuperarTodosPessoaUseCase
    {
        Task<RespostaPessoasJson> Executar();
    }
