namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio;

public class SistemaDeCadastroContext(DbContextOptions<SistemaDeCadastroContext> options) : DbContext(options)
{
    public DbSet<Cadastro> Cadastros { get; set; }

    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaDeCadastroContext).Assembly);
    }
}
