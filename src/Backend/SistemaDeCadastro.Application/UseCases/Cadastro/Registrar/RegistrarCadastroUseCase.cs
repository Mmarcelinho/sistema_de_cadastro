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

        var cadastro = CadastroMap.ConverterParaEntidade(requisicao);

        await _repositorioWrite.Registrar(cadastro);

        await _unidadeDeTrabalho.Commit();

        return CadastroMap.ConverterParaResposta(cadastro);
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