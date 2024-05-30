namespace SistemaDeCadastro.Exceptions;

public static class CadastroMensagensDeErro
{
    public static string CADASTRO_EMAIL_JA_REGISTRADO = "O e-mail informado já está registrado na base de dados.";

    public static string CADASTRO_EMAIL_EMBRANCO = "O e-mail deve ser informado.";

    public static string CADASTRO_EMAIL_INVALIDO = "O e-mail informado é inválido.";

    public static string CADASTRO_NOME_FANTASIA_EMBRANCO = "O nome fantasia deve ser informado.";

    public static string CADASTRO_CREDENCIAL_SENHA_EMBRANCO = "A senha da credencial deve ser informada.";

    public static string CADASTRO_DOCUMENTO_NUMERO_EMBRANCO = "O número do documento deve ser informado.";

    public static string CADASTRO_DOCUMENTO_ORGAO_EMISSOR_EMBRANCO = "O órgão emissor do documento deve ser informado.";

    public static string CADASTRO_DOCUMENTO_DATA_VALIDADE_INVALIDA = "A data de validade do documento é inválida.";

    public static string CADASTRO_IDENTIFICADOR_EMBRANCO = "O identificador deve ser informado.";
    
    public static string CADASTRO_EMPRESA_INVALIDA = "O campo empresa deve ser verdadeiro ou falso.";
}
