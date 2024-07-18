namespace CommonTestUtilities.Repositorios.Cadastro;

public class CadastroWriteOnlyRepositorioBuilder
{
    public static ICadastroWriteOnlyRepositorio Instancia()
    {
        var mock = new Mock<ICadastroWriteOnlyRepositorio>();

        return mock.Object;
    }
}
