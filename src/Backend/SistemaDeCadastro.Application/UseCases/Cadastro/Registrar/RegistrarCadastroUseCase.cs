namespace SistemaDeCadastro.Application.UseCases.Cadastro.Registrar;

public class RegistrarCadastroUseCase(ICadastroWriteOnlyRepositorio repositorioWrite, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho) : IRegistrarCadastroUseCase
{
    public async Task<RespostaCadastroJson> Executar(RequisicaoCadastroJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = CadastroMap.ConverterParaEntidade(requisicao);

        await repositorioWrite.Registrar(cadastro);

        await unidadeDeTrabalho.Commit();

        return CadastroMap.ConverterParaResposta(cadastro);
    }

    private async Task Validar(RequisicaoCadastroJson requisicao)
    {
        var validator = new CadastroValidator();
        var resultado = validator.Validate(requisicao);

        var existeCadastroComEmail = await repositorioRead.RecuperarCadastroExistentePorEmail(requisicao.Email);
        if (existeCadastroComEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}