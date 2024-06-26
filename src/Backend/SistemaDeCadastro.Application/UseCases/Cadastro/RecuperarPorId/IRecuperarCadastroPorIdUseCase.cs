namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarPorId;

    public interface IRecuperarCadastroPorIdUseCase
    {
        Task<RespostaCadastroJson> Executar(long cadastroId);
    }
