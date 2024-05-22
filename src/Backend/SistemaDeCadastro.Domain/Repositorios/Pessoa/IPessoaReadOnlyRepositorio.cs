namespace SistemaDeCadastro.Domain.Repositorios.Pessoa;

    public interface IPessoaReadOnlyRepositorio
    {
        Task<IEnumerable<Entidades.Pessoa>> RecuperarTodos();

        Task<Entidades.Pessoa> RecuperarPorId(long pessoaId);
    }
