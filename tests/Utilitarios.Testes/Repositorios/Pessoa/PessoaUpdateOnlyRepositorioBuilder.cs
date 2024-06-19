namespace Utilitarios.Testes.Repositorios.Pessoa;

public class PessoaUpdateOnlyRepositorioBuilder
{
    private readonly Mock<IPessoaUpdateOnlyRepositorio> _repositorio;

    public PessoaUpdateOnlyRepositorioBuilder() => _repositorio = new Mock<IPessoaUpdateOnlyRepositorio>();

    public PessoaUpdateOnlyRepositorioBuilder RecuperarPorId(SistemaDeCadastro.Domain.Entidades.Pessoa? pessoa)
    {
        if (pessoa is not null)
            _repositorio.Setup(repositorio => repositorio.RecuperarPorId(pessoa.Id)).ReturnsAsync(pessoa);

        return this;
    }

    public IPessoaUpdateOnlyRepositorio Build() => _repositorio.Object;
}
