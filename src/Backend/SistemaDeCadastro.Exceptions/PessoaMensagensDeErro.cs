namespace SistemaDeCadastro.Exceptions;

public static class PessoaMensagensDeErro
{
    public static string PESSOA_CPF_JA_REGISTRADO = "O CPF informado já está registrado na base de dados.";

    public const string PESSOA_CPF_EMBRANCO = "O CPF da pessoa deve ser informado.";

    public const string PESSOA_CPF_INVALIDO = "O CPF da pessoa é inválido.";

    public static string PESSOA_CNPJ_JA_REGISTRADO = "O CNPJ informado já está registrado na base de dados.";

    public const string PESSOA_CNPJ_INVALIDO = "O CNPJ da pessoa é inválido.";

    public const string PESSOA_NOME_EMBRANCO = "O nome da pessoa deve ser informado.";

    public const string PESSOA_NOME_FANTASIA_EMBRANCO = "O nome fantasia da pessoa deve ser informado.";

    public const string PESSOA_EMAIL_EMBRANCO = "O email da pessoa deve ser informado.";

    public const string PESSOA_EMAIL_INVALIDO = "O email da pessoa é inválido.";

    public const string PESSOA_DATA_NASCIMENTO_INVALIDA = "A data de nascimento da pessoa é inválida.";

    public const string PESSOA_TOKEN_INVALIDO = "O token da pessoa é inválido.";

    public const string PESSOA_DOMICILIOS_EMBRANCO = "Os domicílios da pessoa devem ser informados.";

    public const string PESSOA_DOMICILIO_CEP_EMBRANCO = "O CEP do domicílio deve ser informado.";

    public const string PESSOA_TELEFONE_EMBRANCO = "O número de telefone deve ser informado.";

    public const string PESSOA_CADASTRO_EMBRANCO = "O cadastro da pessoa deve ser informado.";
}
