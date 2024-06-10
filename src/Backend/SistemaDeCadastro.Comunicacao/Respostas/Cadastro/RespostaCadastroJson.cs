using SistemaDeCadastro.Comunicacao.Enum;
namespace SistemaDeCadastro.Comunicacao.Respostas.Cadastro;

public record RespostaCadastroJson(
string Id,
string DataCriacao,
string Email,
string NomeFantasia,
string SobrenomeSocial,
bool Empresa,
RespostaCredencialJson Credencial,
RespostaInscritoJson Inscrito,
RespostaParceiroJson Parceiro,
RespostaDocumentoJson Documento,
RespostaIdentificacaoJson Identificador);

public record RespostaCredencialJson(
bool Bloqueada,
string Expirada,
string Senha
);

public record RespostaInscritoJson(
    bool Assinante,
    bool Associado,
    string Senha
);

public record RespostaParceiroJson(
    bool Cliente,
    bool Fornecedor,
    bool Prestador,
    bool Colaborador
);

public record RespostaDocumentoJson(
    string Numero,
    string OrgaoEmissor,
    string EstadoEmissor,
    string DataValidade
);

public record RespostaIdentificacaoJson(
    int Empresa,
    string Identificador,
    IdentificacaoTipo Tipo
);