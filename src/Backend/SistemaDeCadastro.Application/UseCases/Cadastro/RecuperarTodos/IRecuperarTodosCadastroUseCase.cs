namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarTodos;

    public interface IRecuperarTodosCadastroUseCase
    {
        Task<IEnumerable<RespostaCadastroJson>> Executar();
    }
