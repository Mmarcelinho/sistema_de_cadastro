namespace SistemaDeCadastro.Domain.Servicos.CepService;

public interface ICepService
{
    bool ValidarCep(string cep);

    Task<EnderecoJson> RecuperarEndereco(string cep);
}