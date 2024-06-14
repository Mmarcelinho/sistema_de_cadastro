namespace SistemaDeCadastro.Domain.Repositorios.Pessoa;

    public interface IPessoaReadOnlyRepositorio
    {
        Task<IEnumerable<Entidades.Pessoa>> RecuperarTodos();

        Task<Entidades.Pessoa> RecuperarPorId(long pessoaId);

        Task<bool> RecuperarPessoaExistentePorCpf(string cpf);

        Task<bool> RecuperarPessoaExistentePorCnpj(string cnpj);
    }
