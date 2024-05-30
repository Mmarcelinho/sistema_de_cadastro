namespace SistemaDeCadastro.Domain.Repositorios.Cadastro;

    public interface ICadastroReadOnlyRepositorio
    {
        Task<IEnumerable<Entidades.Cadastro>> RecuperarTodos();

        Task<Entidades.Cadastro> RecuperarPorId(long cadastroId);

        Task<Entidades.Cadastro> RecuperarPorEmail(string email);
    }
