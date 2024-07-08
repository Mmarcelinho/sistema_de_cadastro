namespace SistemaDeCadastro.Application.UseCases.Pessoa.Atualizar;

    public interface IAtualizarPessoaUseCase
    {
        Task Executar(long pessoaId, RequisicaoPessoaJson requisicao);
    }
