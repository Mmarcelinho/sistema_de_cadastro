namespace SistemaDeCadastro.Application.Servicos.CepServices;

public class CepServices : ICepServices
{
    public bool ValidarCep(string cep) => !string.IsNullOrEmpty(cep) && cep.Length == 8;

    public async Task<Endereco> RecuperarEndereco(string cep)
    {
        if (!ValidarCep(cep))
            throw new ErrosDeValidacaoException(mensagensDeErro: [MensagensDeErro.CEP_INVALIDO]);

        using var client = new HttpClient();

        var resposta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

        var content = await resposta.Content.ReadAsStringAsync();

        var resultado = JsonSerializer.Deserialize<Endereco>(content);

        return resultado;
    }
}

public record Endereco(
    [property: JsonPropertyName("cep")] string Cep,
    [property: JsonPropertyName("logradouro")] string Logradouro,
    [property: JsonPropertyName("complemento")] string Complemento,
    [property: JsonPropertyName("bairro")] string Bairro,
    [property: JsonPropertyName("localidade")] string Localidade,
    [property: JsonPropertyName("uf")] string Uf,
    [property: JsonPropertyName("ibge")] string Ibge,
    [property: JsonPropertyName("gia")] string Gia,
    [property: JsonPropertyName("ddd")] string Ddd,
    [property: JsonPropertyName("siafi")] string Siafi);

