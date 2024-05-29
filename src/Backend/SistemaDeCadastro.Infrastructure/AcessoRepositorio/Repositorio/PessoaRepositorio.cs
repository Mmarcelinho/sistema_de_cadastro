namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class PessoaRepositorio : IPessoaReadOnlyRepositorio, IPessoaUpdateOnlyRepositorio, IPessoaWriteOnlyRepositorio
{
    private readonly IDbConnection _dbConnection;

    public PessoaRepositorio(IDbConnection dbConnection) => _dbConnection = dbConnection;
    

    public async Task<IEnumerable<Pessoa>> RecuperarTodos()
    {
        var query = PessoaQueries.RecuperarTodosQuery();
        var resultado = await _dbConnection.QueryAsync<dynamic>(query.Query, query.Parameters);
        return resultado.Select(MapToPessoa);
    }

    public async Task<Pessoa> RecuperarPorId(long pessoaId)
    {
        var query = PessoaQueries.RecuperarPorIdQuery(pessoaId);
        var resultado = await _dbConnection.QueryFirstOrDefaultAsync<dynamic>(query.Query, query.Parameters);
        return MapToPessoa(resultado);
    }

    public async Task Registrar(Pessoa pessoa)
    {
        var query = PessoaQueries.InserirPessoaQuery(pessoa);
        await _dbConnection.ExecuteAsync(query.Query, query.Parameters);
    }

    public async Task Deletar(long pessoaId)
    {
        var query = PessoaQueries.DeletarPessoaQuery(pessoaId);
        await _dbConnection.ExecuteAsync(query.Query, query.Parameters);
    }

    public void Atualizar(Pessoa pessoa)
    {
        var query = PessoaQueries.AtualizarPessoaQuery(pessoa);
        _dbConnection.Execute(query.Query, query.Parameters);
    }

    private Pessoa MapToPessoa(dynamic resultado)
    {
        if (resultado == null) return null;

        return new Pessoa
        {
            Id = resultado.Id,
            Cpf = resultado.Cpf,
            Cnpj = resultado.Cnpj,
            Nome = resultado.Nome,
            Nascimento = resultado.Nascimento,
            Token = resultado.Token
        };
    }
}
