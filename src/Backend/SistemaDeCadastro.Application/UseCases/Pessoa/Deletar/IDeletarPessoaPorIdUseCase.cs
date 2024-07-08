namespace SistemaDeCadastro.Application.UseCases.Pessoa.Deletar;

public interface IDeletarPessoaPorIdUseCase
{
    Task Executar(long pessoaId);
}
