namespace SistemaDeCadastro.Domain.Repositorios.Cadastro;

    public interface ICadastroWriteOnlyRepositorio
    {
        Task Registrar(Entidades.Cadastro cadastro);

        Task<bool> Deletar(long cadastroId);
    }
