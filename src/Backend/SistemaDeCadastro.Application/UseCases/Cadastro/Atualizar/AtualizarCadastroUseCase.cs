namespace SistemaDeCadastro.Application.UseCases.Cadastro.Atualizar;

public class AtualizarCadastroUseCase(ICadastroUpdateOnlyRepositorio repositorioUpdate, ICadastroReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho) : IAtualizarCadastroUseCase
{
    public async Task Executar(long cadastroId, RequisicaoCadastroJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = await repositorioUpdate.RecuperarPorId(cadastroId);

        if (cadastro is null)
            throw new NaoEncontradoException(CadastroMensagensDeErro.CADASTRO_NAO_ENCONTRADO);

        cadastro = cadastro.Atualizar(requisicao);

        repositorioUpdate.Atualizar(cadastro);

        await unidadeDeTrabalho.Commit();
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
