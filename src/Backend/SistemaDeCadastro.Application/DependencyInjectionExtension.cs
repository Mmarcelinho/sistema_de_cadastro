namespace SistemaDeCadastro.Application;

public static class DependencyInjectionExtension
{
    public static void AdicionarApplication(this IServiceCollection services)
    {
        AdicionarUseCases(services);
    }

    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarCadastroUseCase, RegistrarCadastroUseCase>();
        services.AddScoped<IRecuperarTodosCadastroUseCase, RecuperarTodosCadastroUseCase>();
        services.AddScoped<IRecuperarCadastroPorIdUseCase, RecuperarCadastroPorIdUseCase>();
        services.AddScoped<IDeletarCadastroPorIdUseCase, DeletarCadastroPorIdUseCase>();
        
        services.AddScoped<IRegistrarPessoaUseCase, RegistrarPessoaUseCase>();
        services.AddScoped<IRecuperarTodosPessoaUseCase, RecuperarTodosPessoaUseCase>();
        services.AddScoped<IRecuperarPessoaPorIdUseCase, RecuperarPessoaPorIdUseCase>();
    }
}
