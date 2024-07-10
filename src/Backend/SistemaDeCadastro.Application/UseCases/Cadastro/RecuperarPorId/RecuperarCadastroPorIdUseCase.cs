using SistemaDeCadastro.Application.Mappings;

namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdUseCase : IRecuperarCadastroPorIdUseCase
{
    private readonly ICadastroReadOnlyRepositorio _repositorio;

    public RecuperarCadastroPorIdUseCase(ICadastroReadOnlyRepositorio repositorio) => _repositorio = repositorio;

    public async Task<RespostaCadastroJson> Executar(long cadastroId)
    {
        var cadastro = await _repositorio.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        return CadastroMap.ConverterParaResposta(cadastro);
    }
}
