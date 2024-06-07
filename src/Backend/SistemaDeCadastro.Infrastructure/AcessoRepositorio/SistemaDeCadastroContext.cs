namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio;

    public class SistemaDeCadastroContext : DbContext
    {
        public SistemaDeCadastroContext(DbContextOptions<SistemaDeCadastroContext> options) : base(options) { }

        public DbSet<Cadastro> Cadastros { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaDeCadastroContext).Assembly);
    }
    }
