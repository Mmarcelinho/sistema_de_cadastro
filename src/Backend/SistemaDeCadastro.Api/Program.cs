

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDeExcecao)));

builder.Services.AdicionarInfrastructure(builder.Configuration);
builder.Services.AdicionarApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

if (builder.Configuration.IsTestEnvironment())
{
    await AtualizarBaseDeDados();
}

app.Run();

async Task AtualizarBaseDeDados()
{
    await using var scope = app.Services.CreateAsyncScope();

    await MigrateExtension.MigrateBancoDeDados(scope.ServiceProvider);
}

public partial class Program { }