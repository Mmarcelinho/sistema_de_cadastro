namespace SistemaDeCadastro.Domain.Servicos.ViaCep;

public interface IViaCep
{
    bool ValidarCep(string cep);

    Task<EnderecoJson> RecuperarEndereco(string cep);
}