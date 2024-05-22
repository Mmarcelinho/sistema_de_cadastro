namespace SistemaDeCadastro.Infrastructure;

public static class Bootstrapper
{
    public static void AdicionarInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarFluentMigrator(services, configuration);
    }

    private static void AdicionarFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddSqlServer()
        .WithGlobalConnectionString(configuration.GetConexaoCompleta())
        .ScanIn(Assembly.Load("SistemaDeCadastro.Infrastructure")).For.All());
    }
}
