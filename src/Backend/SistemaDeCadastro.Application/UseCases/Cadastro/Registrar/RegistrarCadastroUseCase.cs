namespace SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;

public class RegistrarCadastroUseCase : IRegistrarCadastroUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioWrite;

    private readonly ICadastroReadOnlyRepositorio _repositorioRead;

    private IUnidadeDeTrabalho _unidadeDeTrabalho;

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
                Bloqueada = requisicao.CredencialBloqueada,
                Expirada = requisicao.CredencialExpirada,
                Senha = requisicao.CredencialSenha,
            },
            Inscrito = new Inscrito
            {
                Assinante = requisicao.InscritoAssinante,
                Associado = requisicao.InscritoAssociado,
                Senha = requisicao.InscritoSenha
            },
            Parceiro = new Parceiro
            {
                Cliente = requisicao.ParceiroCliente,
                Fornecedor = requisicao.ParceiroFornecedor,
                Prestador = requisicao.ParceiroPrestador,
                Colaborador = requisicao.ParceiroColaborador
            },
            Documento = new Documento(
                        requisicao.DocumentoNumero,
                        requisicao.DocumentoOrgaoEmissor,
                        requisicao.DocumentoEstadoEmissor,
                        requisicao.DocumentoDataValidade
                    ),
            Identificador = new Identificacao(
                        requisicao.IdentificacaoEmpresa,
                        requisicao.IdentificacaoIdentificador,
                        (IdentificacaoTipo)requisicao.IdentificacaoTipo
                    )
        };

        await _repositorioWrite.Registrar(cadastro);

        await _unidadeDeTrabalho.Commit();

        return new RespostaCadastroJson(
                    cadastro.Id.ToString(),
                    cadastro.Email,
                    cadastro.NomeFantasia,
                    cadastro.SobrenomeSocial,
                    cadastro.Empresa,
                    cadastro.Credencial.Bloqueada,
                    cadastro.Credencial.Expirada,
                    cadastro.Credencial.Senha,
                    cadastro.Inscrito.Assinante,
                    cadastro.Inscrito.Associado,
                    cadastro.Inscrito.Senha,
                    cadastro.Parceiro.Cliente,
                    cadastro.Parceiro.Fornecedor,
                    cadastro.Parceiro.Prestador,
                    cadastro.Parceiro.Colaborador,
                    cadastro.Documento.Numero,
                    cadastro.Documento.OrgaoEmissor,
                    cadastro.Documento.EstadoEmissor,
                    cadastro.Documento.DataValidade.ToShortDateString(),
                    cadastro.Identificador.Empresa,
                    cadastro.Identificador.Identificador,
                    (short)cadastro.Identificador.Tipo
                );
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
