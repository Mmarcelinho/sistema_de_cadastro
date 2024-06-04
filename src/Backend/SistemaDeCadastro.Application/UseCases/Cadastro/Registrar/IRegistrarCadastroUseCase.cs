namespace SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;

    public interface IRegistrarCadastroUseCase
    {
        Task<RespostaCadastroJson> Executar(RequisicaoCadastroJson requisicao);
    }
