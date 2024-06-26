namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarPorId;

    public interface IRecuperarPessoaPorIdUseCase
    {
        Task<RespostaPessoaJson> Executar(long pessoaId);
    }
