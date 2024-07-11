namespace CommonTestUtilities.Repositorios;

public static class UnidadeDeTrabalhoBuilder
{
    public static IUnidadeDeTrabalho Build()
    {
        var mock = new Mock<IUnidadeDeTrabalho>();

        return mock.Object;
    }
}
