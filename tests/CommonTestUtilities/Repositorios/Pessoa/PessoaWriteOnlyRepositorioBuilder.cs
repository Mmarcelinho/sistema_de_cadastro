namespace CommonTestUtilities.Repositorios.Pessoa;

    public class PessoaWriteOnlyRepositorioBuilder
    {
        public static IPessoaWriteOnlyRepositorio Instancia()
        {
            var mock = new Mock<IPessoaWriteOnlyRepositorio>();

            return mock.Object;
        }
    }
