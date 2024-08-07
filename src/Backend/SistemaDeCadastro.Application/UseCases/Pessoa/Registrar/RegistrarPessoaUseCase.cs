namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaUseCase(ICadastroWriteOnlyRepositorio repositorioWriteCadastro, ICadastroReadOnlyRepositorio repositorioReadCadastro, IPessoaReadOnlyRepositorio repositorioReadPessoa, IPessoaWriteOnlyRepositorio repositorioWritePessoa, ICepService cepService, IUnidadeDeTrabalho unidadeDeTrabalho) : IRegistrarPessoaUseCase
{
    public async Task<RespostaPessoaJson> Executar(RequisicaoPessoaJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = CadastroMap.ConverterParaEntidade(requisicao.Cadastro);
        var pessoa = PessoaMap.ConverterParaEntidade(requisicao, cadastro);

        pessoa.Domicilios = await PessoaMap.RecuperarEndereco(requisicao, cepService);

        await repositorioWriteCadastro.Registrar(cadastro);
        await repositorioWritePessoa.Registrar(pessoa);

        await unidadeDeTrabalho.Commit();

        return PessoaMap.ConverterParaResposta(pessoa);
    }

    private async Task Validar(RequisicaoPessoaJson requisicao)
    {
        var validator = new PessoaValidator();
        var resultado = validator.Validate(requisicao);

        var existePessoaComCpf = await repositorioReadPessoa.RecuperarPessoaExistentePorCpf(requisicao.Cpf);

        var existePessoaComCnpj = await repositorioReadPessoa.RecuperarPessoaExistentePorCnpj(requisicao.Cnpj);

        var existeCadastroComEmail = await repositorioReadCadastro.RecuperarCadastroExistentePorEmail(requisicao.Cadastro.Email);

        if (existePessoaComCpf)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", PessoaMensagensDeErro.PESSOA_CPF_JA_REGISTRADO));

        else if (existePessoaComCnpj)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));

        else if (existeCadastroComEmail)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", CadastroMensagensDeErro.CADASTRO_EMAIL_JA_REGISTRADO));

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
