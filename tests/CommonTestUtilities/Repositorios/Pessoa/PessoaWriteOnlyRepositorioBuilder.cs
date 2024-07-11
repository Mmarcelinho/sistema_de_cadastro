namespace CommonTestUtilities.Repositorios.Pessoa;

    public class PessoaWriteOnlyRepositorioBuilder
    {
        public static IPessoaWriteOnlyRepositorio Build()
        {
            var mock = new Mock<IPessoaWriteOnlyRepositorio>();

            return mock.Object;
        }
    }
