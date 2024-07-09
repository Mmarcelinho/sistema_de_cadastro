using SistemaDeCadastro.Application.Mappings;

namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdUseCase : IRecuperarCadastroPorIdUseCase
{
    private readonly ICadastroReadOnlyRepositorio _repositorio;

    private readonly ICachingService _cache;

    public RecuperarCadastroPorIdUseCase(ICadastroReadOnlyRepositorio repositorio, ICachingService cache)
    {
        _repositorio = repositorio;
        _cache = cache;
    }
        
    public async Task<RespostaCadastroJson> Executar(long cadastroId)
    {
        string? cadastroCache = await _cache.Recuperar(cadastroId.ToString());
        Domain.Entidades.Cadastro? cadastro;

        if(string.IsNullOrEmpty(cadastroCache))
        {
            cadastro = await _repositorio.RecuperarPorId(cadastroId);

            if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

            await _cache.Registrar(cadastroId.ToString(), JsonConvert.SerializeObject(cadastro));

            return CadastroMap.ConverterParaResposta(cadastro);
        }

        cadastro = JsonConvert.DeserializeObject<Domain.Entidades.Cadastro>(cadastroCache);

        return CadastroMap.ConverterParaResposta(cadastro);
    }
}
