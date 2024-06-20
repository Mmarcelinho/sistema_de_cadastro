namespace SistemaDeCadastro.Infrastructure.Migrations;

public static class MigrateExtension
{
    public async static Task MigrateBancoDeDados(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<SistemaDeCadastroContext>();

        await dbContext.Database.MigrateAsync();
    }
}
