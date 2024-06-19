namespace Utilitarios.Testes.Servicos;

public class ViaCepBuilder
{
    public static IViaCep Build()
    {
        var mock = new Mock<IViaCep>();

        mock.Setup(service => service.ValidarCep(It.IsAny<string>()))
            .Returns(true);

        mock.Setup(service => service.RecuperarEndereco(It.IsAny<string>()))
            .ReturnsAsync((string cep) =>
                new EnderecoJson(
                    Cep: "12345678",
                    Logradouro: "Rua Fictícia",
                    Complemento: "Apt 101",
                    Bairro: "Bairro Fictício",
                    Localidade: "Cidade Fictícia",
                    Uf: "SP",
                    Ibge: "1234567",
                    Gia: "1234",
                    Ddd: "11",
                    Siafi: "5678"
                ));

        return mock.Object;
    }
}
