namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Queries;

public static class PessoaQueries
{
    public static QueryModel RecuperarTodosQuery()
    {
        string tabela = ContextMappings.RecuperarTabelaPessoa();

        string query = @$"SELECT 
                    p.Id,
                    p.DataCriacao,
                    p.Cpf,
                    p.Cnpj,
                    p.Nome,
                    p.Nascimento,
                    p.Token,
                    c.Email,
                    c.NomeFantasia,
                    c.SobrenomeSocial,
                    c.Empresa,
                    c.CredencialBloqueada,
                    c.CredencialExpirada,
                    c.CredencialSenha,
                    c.InscritoAssinante,
                    c.InscritoAssociado,
                    c.InscritoSenha,
                    c.ParceiroCliente,
                    c.ParceiroFornecedor,
                    c.ParceiroPrestador,
                    c.ParceiroColaborador,
                    c.DocumentoNumero,
                    c.DocumentoOrgaoEmissor,
                    c.DocumentoEstadoEmissor,
                    c.DocumentoDataValidade,
                    c.IdentificacaoEmpresa,
                    c.IdentificacaoIdentificador,
                    c.IdentificacaoTipo
                FROM 
                    {tabela} p
                INNER JOIN 
                    Cadastros c WITH (READPAST) ON p.CadastroId = c.Id;";

        var parameters = new { };

        return new QueryModel(query, parameters);
    }

    public static QueryModel RecuperarPorIdQuery(long id)
    {
        string tabela = ContextMappings.RecuperarTabelaPessoa();

        string query = @$"SELECT 
                    p.Id,
                    p.DataCriacao,
                    p.Cpf,
                    p.Cnpj,
                    p.Nome,
                    p.Nascimento,
                    p.Token,
                    c.Email,
                    c.NomeFantasia,
                    c.SobrenomeSocial,
                    c.Empresa,
                    c.CredencialBloqueada,
                    c.CredencialExpirada,
                    c.CredencialSenha,
                    c.InscritoAssinante,
                    c.InscritoAssociado,
                    c.InscritoSenha,
                    c.ParceiroCliente,
                    c.ParceiroFornecedor,
                    c.ParceiroPrestador,
                    c.ParceiroColaborador,
                    c.DocumentoNumero,
                    c.DocumentoOrgaoEmissor,
                    c.DocumentoEstadoEmissor,
                    c.DocumentoDataValidade,
                    c.IdentificacaoEmpresa,
                    c.IdentificacaoIdentificador,
                    c.IdentificacaoTipo
                FROM 
                    {tabela} p
                INNER JOIN 
                    Cadastros c WITH (READPAST) ON p.CadastroId = c.Id
                WHERE 
                    p.Id = @Id;";

        var parameters = new
        {
            Id = id
        };

        return new QueryModel(query, parameters);
    }

    public static QueryModel InserirPessoaQuery(Pessoa pessoa)
    {
        string tabela = ContextMappings.RecuperarTabelaPessoa();

        string query = @$"INSERT INTO {tabela} 
                        (Cpf, Cnpj, Nome, Nascimento, Token, CadastroId) 
                        VALUES 
                        (@Cpf, @Cnpj, @Nome, @Nascimento, @Token, @CadastroId);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

        var parameters = new
        {
            pessoa.Cpf,
            pessoa.Cnpj,
            pessoa.Nome,
            pessoa.Nascimento,
            pessoa.Token,
            pessoa.CadastroId
        };

        return new QueryModel(query, parameters);
    }

    public static QueryModel AtualizarPessoaQuery(Pessoa pessoa)
    {
        string tabela = ContextMappings.RecuperarTabelaPessoa();

        string query = @$"UPDATE {tabela} SET
                        Cpf = @Cpf,
                        Cnpj = @Cnpj,
                        Nome = @Nome,
                        Nascimento = @Nascimento,
                        Token = @Token";

        var parameters = new
        {
            pessoa.Cpf,
            pessoa.Cnpj,
            pessoa.Nome,
            pessoa.Nascimento,
            pessoa.Token
        };

        return new QueryModel(query, parameters);
    }

    public static QueryModel DeletarPessoaQuery(long id)
    {
        string tabela = ContextMappings.RecuperarTabelaPessoa();

        string query = @$"DELETE FROM {tabela} WHERE Id = @Id";

        var parameters = new
        {
            Id = id
        };

        return new QueryModel(query, parameters);
    }
}

