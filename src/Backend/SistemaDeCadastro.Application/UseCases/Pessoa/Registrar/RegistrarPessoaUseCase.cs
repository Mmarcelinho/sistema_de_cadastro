namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaUseCase : IRegistrarPessoaUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioWriteCadastro;

    private readonly ICadastroReadOnlyRepositorio _repositorioReadCadastro;

    private readonly IPessoaWriteOnlyRepositorio _repositorioWrite;

    private readonly IPessoaReadOnlyRepositorio _repositorioRead;

    private readonly IViaCep _viaCep;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RegistrarPessoaUseCase(ICadastroWriteOnlyRepositorio repositorioWriteCadastro, ICadastroReadOnlyRepositorio repositorioReadCadastro, IPessoaReadOnlyRepositorio repositorioRead, IPessoaWriteOnlyRepositorio repositorioWrite, IViaCep viaCep, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioWriteCadastro = repositorioWriteCadastro;
        _repositorioReadCadastro = repositorioReadCadastro;
        _repositorioWrite = repositorioWrite;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _viaCep = viaCep;
    }

    public async Task<RespostaPessoaJson> Executar(RequisicaoPessoaJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = CadastroMap.ConverterParaEntidade(requisicao.Cadastro);
        var pessoa = PessoaMap.ConverterParaEntidade(requisicao, cadastro);

        pessoa.Domicilios = await PessoaMap.RecuperarEndereco(requisicao, _viaCep);

        await _repositorioWriteCadastro.Registrar(cadastro);
        await _repositorioWrite.Registrar(pessoa);

        await _unidadeDeTrabalho.Commit();

        return PessoaMap.ConverterParaResposta(pessoa);
    }

    private async Task Validar(RequisicaoPessoaJson requisicao)
    {
        var validator = new RegistrarPessoaValidator();
        var resultado = validator.Validate(requisicao);

        var existePessoaComCpf = await _repositorioRead.RecuperarPessoaExistentePorCpf(requisicao.Cpf);

        var existePessoaComCnpj = await _repositorioRead.RecuperarPessoaExistentePorCnpj(requisicao.Cnpj);

        var existeCadastroComEmail = await _repositorioReadCadastro.RecuperarCadastroExistentePorEmail(requisicao.Cadastro.Email);

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
