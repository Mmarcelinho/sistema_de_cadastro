namespace SistemaDeCadastro.Application.UseCases.Cadastro.Atualizar;

public class AtualizarCadastroUseCase : IAtualizarCadastroUseCase
{
    private readonly ICadastroUpdateOnlyRepositorio _repositorioUpdate;

    private readonly ICadastroReadOnlyRepositorio _repositorioRead;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AtualizarCadastroUseCase(ICadastroUpdateOnlyRepositorio repositorioUpdate, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioUpdate = repositorioUpdate;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long cadastroId, RequisicaoCadastroJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = await _repositorioUpdate.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        cadastro = Atualizar(cadastro, requisicao);

        _repositorioUpdate.Atualizar(cadastro);

        await _unidadeDeTrabalho.Commit();
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

    private SistemaDeCadastro.Domain.Entidades.Cadastro Atualizar(SistemaDeCadastro.Domain.Entidades.Cadastro cadastro, RequisicaoCadastroJson requisicao)
    {
        cadastro.Email = requisicao.Email;
        cadastro.SobrenomeSocial = requisicao.SobrenomeSocial;
        cadastro.Empresa = requisicao.Empresa;
        cadastro.Credencial = new Credencial
        {
            Bloqueada = requisicao.Credencial.Bloqueada,
            Expirada = requisicao.Credencial.Expirada,
            Senha = requisicao.Credencial.Senha,
        };
        cadastro.Inscrito = new Inscrito
        {
            Assinante = requisicao.Inscrito.Assinante,
            Associado = requisicao.Inscrito.Associado,
            Senha = requisicao.Inscrito.Senha
        };
        cadastro.Parceiro = new Parceiro
        {
            Cliente = requisicao.Parceiro.Cliente,
            Fornecedor = requisicao.Parceiro.Fornecedor,
            Prestador = requisicao.Parceiro.Prestador,
            Colaborador = requisicao.Parceiro.Colaborador
        };
        cadastro.Documento = new Documento(
                    requisicao.Documento.Numero,
                    requisicao.Documento.OrgaoEmissor,
                    requisicao.Documento.EstadoEmissor,
                    requisicao.Documento.DataValidade
                );
        cadastro.Identificador = new Identificacao(
                    requisicao.Identificador.Empresa,
                    requisicao.Identificador.Identificador,
                    (IdentificacaoTipo)requisicao.Identificador.Tipo
                );
        return cadastro;
    }
}
