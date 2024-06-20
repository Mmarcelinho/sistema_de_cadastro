namespace SistemaDeCadastro.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("BancoDeDadosInMemory");
    }
}
