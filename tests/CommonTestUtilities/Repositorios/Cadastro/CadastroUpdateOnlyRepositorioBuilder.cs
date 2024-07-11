namespace CommonTestUtilities.Repositorios.Cadastro;

public class CadastroUpdateOnlyRepositorioBuilder
{
    private readonly Mock<ICadastroUpdateOnlyRepositorio> _repositorio;

    public CadastroUpdateOnlyRepositorioBuilder() => _repositorio = new Mock<ICadastroUpdateOnlyRepositorio>();

    public CadastroUpdateOnlyRepositorioBuilder RecuperarPorId(SistemaDeCadastro.Domain.Entidades.Cadastro? cadastro)
    {
        if (cadastro is not null)
            _repositorio.Setup(repositorio => repositorio.RecuperarPorId(cadastro.Id)).ReturnsAsync(cadastro);

        return this;
    }

    public ICadastroUpdateOnlyRepositorio Build() => _repositorio.Object;
}
