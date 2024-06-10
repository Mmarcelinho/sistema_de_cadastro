using SistemaDeCadastro.Comunicacao.Enum;

namespace SistemaDeCadastro.Comunicacao.Requisicoes.Cadastro;

public record RequisicaoCadastroJson(
    string Email,
    string NomeFantasia,
    string SobrenomeSocial,
    bool Empresa,
    RequisicaoCredencialJson Credencial,
    RequisicaoInscritoJson Inscrito,
    RequisicaoParceiroJson Parceiro,
    RequisicaoDocumentoJson Documento,
    RequisicaoIdentificacaoJson Identificador
    );

public record RequisicaoCredencialJson(
    bool Bloqueada,
    string Expirada,
    string Senha
);

public record RequisicaoInscritoJson(
    bool Assinante,
    bool Associado,
    string Senha
);

public record RequisicaoParceiroJson(
    bool Cliente,
    bool Fornecedor,
    bool Prestador,
    bool Colaborador
);

public record RequisicaoDocumentoJson(
    string Numero,
    string OrgaoEmissor,
    string EstadoEmissor,
    DateTime DataValidade
);

public record RequisicaoIdentificacaoJson(
    int Empresa,
    string Identificador,
    IdentificacaoTipo Tipo
);
