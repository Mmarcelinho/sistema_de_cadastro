namespace SistemaDeCadastro.Application.UseCases.Cadastro.RecuperarPorId;

public class RecuperarCadastroPorIdUseCase : IRecuperarCadastroPorIdUseCase
{
    private readonly ICadastroReadOnlyRepositorio _repositorio;

    public RecuperarCadastroPorIdUseCase(ICadastroReadOnlyRepositorio repositorio)
    =>
        _repositorio = repositorio;

    public async Task<RespostaCadastroJson> Executar(long cadastroId)
    {
        var cadastro = await _repositorio.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new Exception(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

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
                (Communication.Enum.IdentificacaoTipo)cadastro.Identificador.Tipo)
        );
    }
}
