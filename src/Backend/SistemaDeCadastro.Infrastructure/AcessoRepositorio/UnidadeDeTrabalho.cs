namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio;

public class UnidadeDeTrabalho : IUnidadeDeTrabalho
{
    private readonly SistemaDeCadastroContext _contexto;

    public UnidadeDeTrabalho(SistemaDeCadastroContext contexto) => _contexto = contexto;
    public async Task Commit() => await _contexto.SaveChangesAsync();
}
