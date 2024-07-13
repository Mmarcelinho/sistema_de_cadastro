namespace SistemaDeCadastro.Infrastructure.Servicos.CepService;

public class ViaCep : ICepService
{
    public bool ValidarCep(string cep) => !string.IsNullOrEmpty(cep) && cep.Length == 8;

    public async Task<EnderecoJson> RecuperarEndereco(string cep)
    {
        if (!ValidarCep(cep))
            throw new ErrosDeValidacaoException(mensagensDeErro: [MensagensDeErro.CEP_INVALIDO]);

        using var client = new HttpClient();

        var resposta = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

        var content = await resposta.Content.ReadAsStringAsync();

        var resultado = JsonSerializer.Deserialize<EnderecoJson>(content);

        return resultado;
    }
}
