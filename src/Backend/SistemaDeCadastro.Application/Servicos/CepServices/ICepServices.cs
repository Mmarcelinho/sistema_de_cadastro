namespace SistemaDeCadastro.Application.Servicos.CepServices;

    public interface ICepServices
    {
        bool ValidarCep(string cep);

        Task<Endereco> RecuperarEndereco(string cep);
    }
