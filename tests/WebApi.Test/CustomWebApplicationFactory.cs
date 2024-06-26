namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private Cadastro? _cadastro;

    private Pessoa? _pessoa;

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

            var cacheMock = new Mock<ICachingService>();

            cacheMock.Setup(x => x.Recuperar(It.IsAny<string>()))
                            .ReturnsAsync((string)null);
            cacheMock.Setup(x => x.Registrar(It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(Task.CompletedTask);

            services.AddSingleton(cacheMock.Object);


            var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SistemaDeCadastroContext>();

            IniciarDatabase(dbContext);
        });
    }

    private void IniciarDatabase(SistemaDeCadastroContext dbContext)
    {
        _cadastro = AdicionarCadastro(dbContext);
        _pessoa = AdicionarPessoa(dbContext, 2);

        dbContext.SaveChanges();
    }

    private Cadastro AdicionarCadastro(SistemaDeCadastroContext dbContext)
    {
        var cadastro = CadastroBuilder.Instancia();

        dbContext.Cadastros.Add(cadastro);

        return cadastro;
    }

    private Pessoa AdicionarPessoa(SistemaDeCadastroContext dbContext, long cadastroId)
    {
        var cadastro = CadastroBuilder.Instancia();
        cadastro.Id = cadastroId;

        var pessoa = PessoaBuilder.Instancia(cadastro);

        dbContext.Pessoas.Add(pessoa);

        return pessoa;
    }

    public Cadastro RecuperarCadastro => _cadastro;

    public Pessoa RecuperarPessoa => _pessoa;
}
