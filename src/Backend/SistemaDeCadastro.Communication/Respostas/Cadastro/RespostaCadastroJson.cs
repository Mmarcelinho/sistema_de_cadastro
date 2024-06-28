namespace SistemaDeCadastro.Communication.Respostas.Cadastro;

public record RespostaCadastroJson(
long Id,
DateTime DataCriacao,
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
    DateTime DataValidade
);

public record RespostaIdentificacaoJson(
    int Empresa,
    string Identificador,
    IdentificacaoTipo Tipo
);

public record RespostaCadastrosJson
{
    public List<RespostaCadastroJson> Cadastros { get; init; }
}