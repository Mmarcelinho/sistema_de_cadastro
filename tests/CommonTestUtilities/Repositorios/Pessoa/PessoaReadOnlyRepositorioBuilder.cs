namespace CommonTestUtilities.Repositorios.Pessoa;

public class PessoaReadOnlyRepositorioBuilder
{
    private readonly Mock<IPessoaReadOnlyRepositorio> _repositorio;

    public PessoaReadOnlyRepositorioBuilder() => _repositorio = new Mock<IPessoaReadOnlyRepositorio>();

    public PessoaReadOnlyRepositorioBuilder RecuperarTodos(List<SistemaDeCadastro.Domain.Entidades.Pessoa> pessoas)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarTodos()).ReturnsAsync(pessoas);

        return this;
    }

    public PessoaReadOnlyRepositorioBuilder RecuperarPorId(SistemaDeCadastro.Domain.Entidades.Pessoa pessoa)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarPorId(pessoa.Id)).ReturnsAsync(pessoa);

        return this;
    }

    public PessoaReadOnlyRepositorioBuilder RecuperarPessoaExistentePorCpf(string cpf)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarPessoaExistentePorCpf(cpf)).ReturnsAsync(true);

        return this;
    }

    public PessoaReadOnlyRepositorioBuilder RecuperarPessoaExistentePorCnpj(string cnpj)
    {
        _repositorio.Setup(repositorio => repositorio.RecuperarPessoaExistentePorCnpj(cnpj)).ReturnsAsync(true);

        return this;
    }

    public IPessoaReadOnlyRepositorio Instancia() => _repositorio.Object;
}
