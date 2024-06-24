namespace SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;

public class RegistrarCadastroUseCase : IRegistrarCadastroUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioWrite;

    private readonly ICadastroReadOnlyRepositorio _repositorioRead;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RegistrarCadastroUseCase(ICadastroWriteOnlyRepositorio repositorioWrite, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioWrite = repositorioWrite;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }
    public async Task<RespostaCadastroJson> Executar(RequisicaoCadastroJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = new Domain.Entidades.Cadastro
        {
            Email = requisicao.Email,
            NomeFantasia = requisicao.NomeFantasia,
            SobrenomeSocial = requisicao.SobrenomeSocial,
            Empresa = requisicao.Empresa,
            Credencial = new Credencial
            {
                Bloqueada = requisicao.Credencial.Bloqueada,
                Expirada = requisicao.Credencial.Expirada,
                Senha = requisicao.Credencial.Senha,
            },
            Inscrito = new Inscrito
            {
                Assinante = requisicao.Inscrito.Assinante,
                Associado = requisicao.Inscrito.Associado,
                Senha = requisicao.Inscrito.Senha
            },
            Parceiro = new Parceiro
            {
                Cliente = requisicao.Parceiro.Cliente,
                Fornecedor = requisicao.Parceiro.Fornecedor,
                Prestador = requisicao.Parceiro.Prestador,
                Colaborador = requisicao.Parceiro.Colaborador
            },
            Documento = new Documento(
                        requisicao.Documento.Numero,
                        requisicao.Documento.OrgaoEmissor,
                        requisicao.Documento.EstadoEmissor,
                        requisicao.Documento.DataValidade
                    ),
            Identificador = new Identificacao(
                        requisicao.Identificador.Empresa,
                        requisicao.Identificador.Identificador,
                        (IdentificacaoTipo)requisicao.Identificador.Tipo
                    )
        };

        await _repositorioWrite.Registrar(cadastro);

        await _unidadeDeTrabalho.Commit();

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

    private async Task Validar(RequisicaoCadastroJson requisicao)
    {
        var validator = new RegistrarCadastroValidator();
        var resultado = validator.Validate(requisicao);

        var existeCadastroComEmail = await _repositorioRead.RecuperarCadastroExistentePorEmail(requisicao.Email);
        if (existeCadastroComEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
