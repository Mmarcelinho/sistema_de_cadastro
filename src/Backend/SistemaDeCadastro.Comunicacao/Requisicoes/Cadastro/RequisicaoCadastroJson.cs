namespace SistemaDeCadastro.Comunicacao.Requisicoes.Cadastro;

public record RequisicaoCadastroJson(
    string Email,
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
    DateTime DocumentoDataValidade,
    int IdentificacaoEmpresa,
    string IdentificacaoIdentificador,
    short IdentificacaoTipo);
