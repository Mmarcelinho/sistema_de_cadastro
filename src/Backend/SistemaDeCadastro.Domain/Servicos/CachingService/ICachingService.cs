namespace SistemaDeCadastro.Domain.Servicos.CachingService;

    public interface ICachingService
    {
        Task Registrar(string chave, string valor);

        Task<string> Recuperar(string chave);
    }
