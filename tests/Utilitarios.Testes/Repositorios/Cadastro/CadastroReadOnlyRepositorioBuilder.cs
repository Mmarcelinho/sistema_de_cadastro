namespace Utilitarios.Testes.Repositorios.Cadastro;

public class CadastroReadOnlyRepositorioBuilder
{
    private readonly Mock<ICadastroReadOnlyRepositorio> _repositorio;

    public CadastroReadOnlyRepositorioBuilder() => _repositorio = new Mock<ICadastroReadOnlyRepositorio>();

    public CadastroReadOnlyRepositorioBuilder RecuperarTodos(List<SistemaDeCadastro.Domain.Entidades.Cadastro> cadastros)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarTodos()).ReturnsAsync(cadastros);

        return this;
    }

    public CadastroReadOnlyRepositorioBuilder RecuperarPorId(SistemaDeCadastro.Domain.Entidades.Cadastro cadastro)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarPorId(cadastro.Id)).ReturnsAsync(cadastro);

        return this;
    }

    public ICadastroReadOnlyRepositorio Build() => _repositorio.Object;
}
