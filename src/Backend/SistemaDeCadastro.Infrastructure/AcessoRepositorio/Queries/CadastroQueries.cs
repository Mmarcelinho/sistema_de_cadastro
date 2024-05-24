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

    public static QueryModel InserirCadastroQuery(Cadastro cadastro)
        {
            string tabela = ContextMappings.RecuperarTabelaCadastro();

            string query = @$"
                INSERT INTO {tabela} (
                    DataCriacao,
                    Email,
                    NomeFantasia,
                    SobrenomeSocial,
                    Empresa,
                    CredencialBloqueada,
                    CredencialExpirada,
                    CredencialSenha,
                    InscritoAssinante,
                    InscritoAssociado,
                    InscritoSenha,
                    ParceiroCliente,
                    ParceiroFornecedor,
                    ParceiroPrestador,
                    ParceiroColaborador,
                    DocumentoNumero,
                    DocumentoOrgaoEmissor,
                    DocumentoEstadoEmissor,
                    DocumentoDataValidade,
                    IdentificacaoEmpresa,
                    IdentificacaoIdentificador,
                    IdentificacaoTipo
                ) VALUES (
                    @DataCriacao,
                    @Email,
                    @NomeFantasia,
                    @SobrenomeSocial,
                    @Empresa,
                    @CredencialBloqueada,
                    @CredencialExpirada,
                    @CredencialSenha,
                    @InscritoAssinante,
                    @InscritoAssociado,
                    @InscritoSenha,
                    @ParceiroCliente,
                    @ParceiroFornecedor,
                    @ParceiroPrestador,
                    @ParceiroColaborador,
                    @DocumentoNumero,
                    @DocumentoOrgaoEmissor,
                    @DocumentoEstadoEmissor,
                    @DocumentoDataValidade,
                    @IdentificacaoEmpresa,
                    @IdentificacaoIdentificador,
                    @IdentificacaoTipo
                ); 
                SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new
            {
                cadastro.DataCriacao,
                cadastro.Email,
                cadastro.NomeFantasia,
                cadastro.SobrenomeSocial,
                cadastro.Empresa,
                CredencialBloqueada = cadastro.Credencial.Bloqueada,
                CredencialExpirada = cadastro.Credencial.Expirada,
                CredencialSenha = cadastro.Credencial.Senha,
                InscritoAssinante = cadastro.Inscrito.Assinante,
                InscritoAssociado = cadastro.Inscrito.Associado,
                InscritoSenha = cadastro.Inscrito.Senha,
                ParceiroCliente = cadastro.Parceiro.Cliente,
                ParceiroFornecedor = cadastro.Parceiro.Fornecedor,
                ParceiroPrestador = cadastro.Parceiro.Prestador,
                ParceiroColaborador = cadastro.Parceiro.Colaborador,
                DocumentoNumero = cadastro.Documento.Numero,
                DocumentoOrgaoEmissor = cadastro.Documento.OrgaoEmissor,
                DocumentoEstadoEmissor = cadastro.Documento.EstadoEmissor,
                DocumentoDataValidade = cadastro.Documento.DataValidade,
                IdentificacaoEmpresa = cadastro.Identificador.Empresa,
                IdentificacaoIdentificador = cadastro.Identificador.Identificador,
                IdentificacaoTipo = cadastro.Identificador.Tipo
            };

            return new QueryModel(query, parameters);
        }
}
