namespace SistemaDeCadastro.Application.Servicos.CepServices;

    public interface ICepServices
    {
        bool ValidarCep(string cep);

        Task<EnderecoJson> RecuperarEndereco(string cep);
    }
