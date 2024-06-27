using SistemaDeCadastro.Domain.Servicos.CachingService;

namespace Utilitarios.Testes.Servicos;

    public class CachingServiceBuilder
    {
        public static ICachingService Build()
        {
            var mock = new Mock<ICachingService>();

            return mock.Object;
        }
    }
