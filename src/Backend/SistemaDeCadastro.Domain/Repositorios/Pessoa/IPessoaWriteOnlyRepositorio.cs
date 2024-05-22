namespace SistemaDeCadastro.Domain.Repositorios.Pessoa;

    public interface IPessoaWriteOnlyRepositorio
    {
        Task Registrar(Entidades.Pessoa pessoa);

        Task Deletar(long pessoaId);
    }
