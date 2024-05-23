using SistemaDeCadastro.Infrastructure.AcessoRepositorio.Map;

namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Queries;

    public static class CadastroQueries
    {
        public static QueryModel RecuperarTodosQuery()
        {
            string tabela = ContextMappings.RecuperarTabelaCadastro();


            string query = @$"SELECT * FROM {tabela}";

            var parameters = new { };

            return new QueryModel(query, parameters);
        }
    }
