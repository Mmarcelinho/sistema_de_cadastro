

namespace SistemaDeCadastro.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AdicionarInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarFluentMigrator(services, configuration);
        AdicionarContexto(services, configuration);
        AdicionarDbConnection(services, configuration);
        AdicionarRepositorios(services);
    }

    private static void AdicionarFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddSqlServer()
        .WithGlobalConnectionString(configuration.GetConexaoCompleta())
        .ScanIn(Assembly.Load("SistemaDeCadastro.Infrastructure")).For.All());
    }

    private static void AdicionarContexto(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConexaoCompleta();

        services.AddDbContext<SistemaDeCadastroContext>(opcoes =>
        {
            opcoes.UseSqlServer(connectionString);
        });
    }

    private static void AdicionarDbConnection(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnection>
        (provider =>
        {
            var connection = new SqlConnection(configuration.GetConexaoCompleta());
            connection.Open();
            return connection;
        });
    }

    private static void AdicionarRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        services.AddScoped<ICadastroReadOnlyRepositorio, CadastroRepositorio>();
        services.AddScoped<ICadastroWriteOnlyRepositorio, CadastroRepositorio>();
        services.AddScoped<ICadastroUpdateOnlyRepositorio, CadastroRepositorio>();
    }
}
