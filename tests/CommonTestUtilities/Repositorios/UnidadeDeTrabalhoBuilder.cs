namespace CommonTestUtilities.Repositorios;

public static class UnidadeDeTrabalhoBuilder
{
    public static IUnidadeDeTrabalho Instancia()
    {
        var mock = new Mock<IUnidadeDeTrabalho>();

        return mock.Object;
    }
}
