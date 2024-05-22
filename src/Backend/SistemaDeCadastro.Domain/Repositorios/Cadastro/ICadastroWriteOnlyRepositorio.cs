namespace SistemaDeCadastro.Domain.Repositorios.Cadastro;

    public interface ICadastroWriteOnlyRepositorio
    {
        Task Registrar(Entidades.Cadastro cadastro);

        Task Deletar(long cadastroId);
    }
