namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
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
        _ = AdicionarCadastro(dbContext);
        _ = AdicionarPessoa(dbContext, 2);

        dbContext.SaveChanges();
    }

    private Cadastro AdicionarCadastro(SistemaDeCadastroContext dbContext)
    {
        var cadastro = CadastroBuilder.Build();
        
        dbContext.Cadastros.Add(cadastro);

        return cadastro;
    }

    private Pessoa AdicionarPessoa(SistemaDeCadastroContext dbContext, long cadastroId)
    {
        var cadastro = CadastroBuilder.Build();
        cadastro.Id = cadastroId;

        var pessoa = PessoaBuilder.Build(cadastro);
        
        dbContext.Pessoas.Add(pessoa);

        return pessoa;
    }

}
