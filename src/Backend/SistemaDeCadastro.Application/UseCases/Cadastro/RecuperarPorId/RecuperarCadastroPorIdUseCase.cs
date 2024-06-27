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

            return MapearCadastroParaResposta(cadastro);
        }

        cadastro = JsonConvert.DeserializeObject<Domain.Entidades.Cadastro>(cadastroCache);

        return MapearCadastroParaResposta(cadastro);
    }

    private static RespostaCadastroJson MapearCadastroParaResposta(Domain.Entidades.Cadastro cadastro)
    {
        return new RespostaCadastroJson(
            cadastro.Id,
            cadastro.DataCriacao,
            cadastro.Email,
            cadastro.NomeFantasia,
            cadastro.SobrenomeSocial,
            cadastro.Empresa,
            new RespostaCredencialJson(
                cadastro.Credencial.Bloqueada,
                cadastro.Credencial.Expirada,
                cadastro.Credencial.Senha),
            new RespostaInscritoJson(
                cadastro.Inscrito.Assinante,
                cadastro.Inscrito.Associado,
                cadastro.Inscrito.Senha),
            new RespostaParceiroJson(
                cadastro.Parceiro.Cliente,
                cadastro.Parceiro.Fornecedor,
                cadastro.Parceiro.Prestador,
                cadastro.Parceiro.Colaborador),
            new RespostaDocumentoJson(
                cadastro.Documento.Numero,
                cadastro.Documento.OrgaoEmissor,
                cadastro.Documento.EstadoEmissor,
                cadastro.Documento.DataValidade),
            new RespostaIdentificacaoJson(
                cadastro.Identificador.Empresa,
                cadastro.Identificador.Identificador,
                (Communication.Enum.IdentificacaoTipo)cadastro.Identificador.Tipo));
    }
}
