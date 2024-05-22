namespace SistemaDeCadastro.Domain.Repositorios.Pessoa;

    public interface IPessoaUpdateOnlyRepositorio
    {
        void Atualizar(Entidades.Pessoa pessoa);

        Task<Entidades.Pessoa> RecuperarPorId(long pessoaId);
    }
