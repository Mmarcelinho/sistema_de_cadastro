namespace SistemaDeCadastro.Application;

public static class DependencyInjectionExtension
{
    public static void AdicionarApplication(this IServiceCollection services)
    {
        AdicionarUseCases(services);
    }

    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<ICepServices, CepServices>();
        services.AddScoped<IRegistrarCadastroUseCase, RegistrarCadastroUseCase>();
    }
}
