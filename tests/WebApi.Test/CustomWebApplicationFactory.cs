namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public Cadastro Cadastro { get; private set; } = default!;

    public Pessoa Pessoa { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.UseEnvironment("Test")
        .ConfigureServices(services =>
        {
            var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            services.AddDbContext<SistemaDeCadastroContext>(config =>
            {
                config.UseInMemoryDatabase("InMemoryDbForTesting");
                config.UseInternalServiceProvider(provider);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SistemaDeCadastroContext>();

            IniciarDatabase(dbContext);
        });
    }

    private void IniciarDatabase(SistemaDeCadastroContext dbContext)
    {
        Cadastro = AdicionarCadastro(dbContext);
        Pessoa = AdicionarPessoa(dbContext, 2);

        dbContext.SaveChanges();
    }

    private static Cadastro AdicionarCadastro(SistemaDeCadastroContext dbContext)
    {
        var cadastro = CadastroBuilder.Instancia();

        dbContext.Cadastros.Add(cadastro);

        return cadastro;
    }

    private static Pessoa AdicionarPessoa(SistemaDeCadastroContext dbContext, long cadastroId)
    {
        var cadastro = CadastroBuilder.Instancia();
        cadastro.Id = cadastroId;

        var pessoa = PessoaBuilder.Instancia(cadastro);

        dbContext.Pessoas.Add(pessoa);

        return pessoa;
    }
}
