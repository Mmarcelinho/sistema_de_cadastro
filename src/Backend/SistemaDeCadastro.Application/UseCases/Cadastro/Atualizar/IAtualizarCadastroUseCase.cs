namespace SistemaDeCadastro.Application.UseCases.Cadastro.Atualizar;

    public interface IAtualizarCadastroUseCase
    {
        Task Executar(long cadastroId, RequisicaoCadastroJson requisicao);
    }
