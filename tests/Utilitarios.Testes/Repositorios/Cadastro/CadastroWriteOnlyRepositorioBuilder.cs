namespace Utilitarios.Testes.Repositorios.Cadastro;

public class CadastroWriteOnlyRepositorioBuilder
{
    public static ICadastroWriteOnlyRepositorio Build()
    {
        var mock = new Mock<ICadastroWriteOnlyRepositorio>();

        return mock.Object;
    }
}
