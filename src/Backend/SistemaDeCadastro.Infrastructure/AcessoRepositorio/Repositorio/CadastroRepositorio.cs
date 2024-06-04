namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class CadastroRepositorio : ICadastroReadOnlyRepositorio, ICadastroWriteOnlyRepositorio, ICadastroUpdateOnlyRepositorio
{
    private readonly IDbConnection _connection;

    public CadastroRepositorio(IDbConnection connection) => _connection = connection;

    public async Task<IEnumerable<Cadastro>> RecuperarTodos()
    {
        var query = CadastroQueries.RecuperarTodosQuery();
        var resultado = await _connection.QueryAsync<dynamic>(query.Query, query.Parameters);
        return resultado.Select(MapToCadastro);
    }

    public async Task<Cadastro> RecuperarPorId(long cadastroId)
    {
        var query = CadastroQueries.RecuperarPorIdQuery(cadastroId);
        var resultado = await _connection.QueryFirstOrDefaultAsync<dynamic>(query.Query, query.Parameters);
        return MapToCadastro(resultado);
    }

    public async Task<bool> RecuperarCadastroExistentePorEmail(string email)
    {
        var query = CadastroQueries.RecuperarCadastroExistentePorEmailQuery(email);
        var count = await _connection.ExecuteScalarAsync<int>(query.Query, query.Parameters);
        return count > 0;
    }

    public async Task Registrar(Cadastro cadastro)
    {
        var query = CadastroQueries.InserirCadastroQuery(cadastro);
        await _connection.ExecuteAsync(query.Query, query.Parameters);
    }

    public void Atualizar(Cadastro cadastro)
    {
        var query = CadastroQueries.AtualizarCadastroQuery(cadastro);
        _connection.Execute(query.Query, query.Parameters);
    }


    public async Task Deletar(long cadastroId)
    {
        var query = CadastroQueries.DeletarCadastroQuery(cadastroId);
        await _connection.ExecuteAsync(query.Query, query.Parameters);
    }

    private Cadastro MapToCadastro(dynamic resultado)
    {
        if (resultado == null) return null;

        return new Cadastro
        {
            Id = resultado.Id,
            Email = resultado.Email,
            NomeFantasia = resultado.SobrenomeSocial,
            Empresa = resultado.Empresa,
            Credencial = new Credencial
            {
                Bloqueada = resultado.CredencialBloqueada,
                Expirada = resultado.CredencialExpirada,
                Senha = resultado.CredencialSenha
            },
            Inscrito = new Inscrito
            {
                Assinante = resultado.InscritoAssinante,
                Associado = resultado.InscritoAssociado,
                Senha = resultado.InscritoSenha
            },
            Parceiro = new Parceiro
            {
                Cliente = resultado.ParceiroCliente,
                Fornecedor = resultado.ParceiroFornecedor,
                Prestador = resultado.ParceiroPrestador,
                Colaborador = resultado.ParceiroColaborador
            },
            Documento = new Documento(
            resultado.DocumentoNumero,
            resultado.DocumentoOrgaoEmissor,
            resultado.DocumentoEstadoEmissor,
            resultado.DocumentoDataValidade
        ),
            Identificador = new Identificacao(
            resultado.IdentificacaoEmpresa,
            resultado.IdentificacaoIdentificador,
            (IdentificacaoTipo)resultado.IdentificacaoTipo
        )

        };

    }
}
