namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarTodos;

    public interface IRecuperarTodosCadastroUseCase
    {
        Task<RespostaCadastrosJson> Executar();
    }
