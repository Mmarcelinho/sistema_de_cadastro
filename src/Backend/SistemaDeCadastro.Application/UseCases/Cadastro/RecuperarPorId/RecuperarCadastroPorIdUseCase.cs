namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdUseCase(ICadastroReadOnlyRepositorio repositorio) : IRecuperarCadastroPorIdUseCase
{
    public async Task<RespostaCadastroJson> Executar(long cadastroId)
    {
        var cadastro = await repositorio.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        return CadastroMap.ConverterParaResposta(cadastro);
    }
}
