namespace SistemaDeCadastro.Application.UseCases.Cadastro.Deletar;

    public interface IDeletarCadastroPorIdUseCase
    {
        Task Executar(long cadastroId);
    }
