namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

    public interface IRegistrarPessoaUseCase
    {
        Task<RespostaPessoaJson> Executar(RequisicaoPessoaJson requisicao);
    }
