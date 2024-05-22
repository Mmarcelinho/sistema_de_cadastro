namespace SistemaDeCadastro.Domain.Repositorios.Cadastro;

    public interface ICadastroUpdateOnlyRepositorio
    {
        void Atualizar(Entidades.Cadastro cadastro);

        Task<Entidades.Cadastro> RecuperarPorId(long cadastroId);
    }
