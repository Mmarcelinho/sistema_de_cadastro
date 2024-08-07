namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio;

public class UnidadeDeTrabalho(SistemaDeCadastroContext contexto) : IUnidadeDeTrabalho
{
    public async Task Commit() => await contexto.SaveChangesAsync();
}
