namespace SistemaDeCadastro.Communication.Respostas.Pessoa;

public record RespostaPessoaJson(
     string Id,
     string Cpf,
     string Cnpj,
     string Nome,
     string NomeFantasia,
     string Email,
     DateTime Nascimento,
     int Token,
     List<RespostaDomicilioJson> Domicilios,
     RespostaTelefoneJson Telefone,
     RespostaCadastroJson Cadastro);

public record RespostaDomicilioJson(
    DomicilioTipo Tipo,
    RespostaEnderecoJson Endereco);

public record RespostaEnderecoJson(
    string Cep,
    string Logradouro,
    string Numero,
    string Bairro,
    string Complemento,
    string PontoReferencia,
    string Uf,
    string Cidade,
    string Ibge);

public record RespostaTelefoneJson(
    long Numero,
    bool Celular,
    bool Whatsapp,
    bool Telegram);
