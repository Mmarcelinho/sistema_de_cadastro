namespace SistemaDeCadastro.Comunicacao.Requisicoes.Pessoa;

public record RequisicaoPessoaJson(
        string Cpf,
        string Cnpj,
        string Nome,
        string NomeFantasia,
        string Email,
        DateTime Nascimento,
        int Token,
        List<RequisicaoDomicilioJson> Domicilios,
        RequisicaoTelefoneJson Telefone,
        RequisicaoCadastroJson Cadastro);

public record RequisicaoDomicilioJson(
        DomicilioTipo Tipo,
        RequisicaoEnderecoJson Endereco);

public record RequisicaoEnderecoJson(
    string Cep,
    string Numero,
    string Complemento,
    string PontoReferencia);

public record RequisicaoTelefoneJson(
    long Numero,
    bool Celular,
    bool Whatsapp,
    bool Telegram);