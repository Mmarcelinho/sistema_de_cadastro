namespace SistemaDeCadastro.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AdicionarInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsTestEnvironment() == false)
            AdicionarContexto(services, configuration);

        AdicionarRepositorios(services);
        AdicionarCaching(services, configuration);
    }

    private static void AdicionarContexto(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Conexao");

        services.AddDbContext<SistemaDeCadastroContext>(opcoes =>
        {
            opcoes.UseNpgsql(connectionString);
        });
    }

    private static void AdicionarRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        services.AddScoped<ICadastroReadOnlyRepositorio, CadastroRepositorio>();
        services.AddScoped<ICadastroWriteOnlyRepositorio, CadastroRepositorio>();
        services.AddScoped<ICadastroUpdateOnlyRepositorio, CadastroRepositorio>();

        services.AddScoped<IPessoaReadOnlyRepositorio, PessoaRepositorio>();
        services.AddScoped<IPessoaWriteOnlyRepositorio, PessoaRepositorio>();
        services.AddScoped<IPessoaUpdateOnlyRepositorio, PessoaRepositorio>();

        services.AddScoped<IViaCep, ViaCep>();
    }

    private static void AdicionarCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");
        services.AddScoped<ICachingService, CachingService>();
        services.AddStackExchangeRedisCache(o =>
        {
            o.InstanceName = "instance";
            o.Configuration = connectionString;
        });
    }
}
