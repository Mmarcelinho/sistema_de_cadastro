namespace SistemaDeCadastro.Comunicacao.Respostas.Cadastro;

    public record RespostaCadastroJson(
    string Id,
    string DataCriacao,
    string NomeFantasia,
    string SobrenomeSocial,
    bool Empresa,
    bool CredencialBloqueada,
    string CredencialExpirada,
    string CredencialSenha,
    bool InscritoAssinante,
    bool InscritoAssociado,
    string InscritoSenha,
    bool ParceiroCliente,
    bool ParceiroFornecedor,
    bool ParceiroPrestador,
    bool ParceiroColaborador,
    string DocumentoNumero,
    string DocumentoOrgaoEmissor,
    string DocumentoEstadoEmissor,
    string DocumentoDataValidade,
    int IdentificacaoEmpresa,
    string IdentificacaoIdentificador,
    short IdentificacaoTipo);