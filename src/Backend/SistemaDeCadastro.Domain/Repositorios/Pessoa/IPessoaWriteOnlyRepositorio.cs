namespace SistemaDeCadastro.Domain.Repositorios.Pessoa;

    public interface IPessoaWriteOnlyRepositorio
    {
        Task Registrar(Entidades.Pessoa pessoa);

        Task<bool> Deletar(long pessoaId);
    }
