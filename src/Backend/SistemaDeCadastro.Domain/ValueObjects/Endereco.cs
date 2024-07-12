namespace SistemaDeCadastro.Domain.ValueObjects;

public record Endereco(string Cep, string Logradouro, string Numero, string Bairro, string Complemento, string PontoReferencia, string Uf, string Cidade, string Ibge);

