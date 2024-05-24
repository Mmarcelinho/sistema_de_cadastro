using SistemaDeCadastro.Domain.Entidades;
using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Map;

namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Queries;

public static class CadastroQueries
{
    public static QueryModel RecuperarTodosQuery()
    {
        string tabela = ContextMappings.RecuperarTabelaCadastro();


        string query = @$"SELECT * FROM {tabela} WITH (READPAST)";

        var parameters = new { };

        return new QueryModel(query, parameters);
    }

    public static QueryModel RecuperarPorIdQuery(long id)
    {
        string tabela = ContextMappings.RecuperarTabelaCadastro();


        string query = @$"SELECT * FROM {tabela} WITH (READPAST) WHERE Id @Id";

        var parameters = new
        {
            Id = id
        };

        return new QueryModel(query, parameters);
    }
}
