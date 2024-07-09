using SistemaDeCadastro.Application.Mappings;

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

        cadastro = cadastro.Atualizar(requisicao);

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
}
