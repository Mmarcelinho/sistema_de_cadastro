namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaUseCase : IRegistrarPessoaUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioWriteCadastro;

    private readonly ICadastroReadOnlyRepositorio _repositorioReadCadastro;

    private readonly IPessoaWriteOnlyRepositorio _repositorioWritePessoa;

    private readonly IPessoaReadOnlyRepositorio _repositorioReadPessoa;

    private readonly ICepService _cepService;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RegistrarPessoaUseCase(ICadastroWriteOnlyRepositorio repositorioWriteCadastro, ICadastroReadOnlyRepositorio repositorioReadCadastro, IPessoaReadOnlyRepositorio repositorioReadPessoa, IPessoaWriteOnlyRepositorio repositorioWritePessoa, ICepService cepService, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioWriteCadastro = repositorioWriteCadastro;
        _repositorioReadCadastro = repositorioReadCadastro;
        _repositorioWritePessoa = repositorioWritePessoa;
        _repositorioReadPessoa = repositorioReadPessoa;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _cepService = cepService;
    }

    public async Task<RespostaPessoaJson> Executar(RequisicaoPessoaJson requisicao)
    {
        await Validar(requisicao);

        var cadastro = CadastroMap.ConverterParaEntidade(requisicao.Cadastro);
        var pessoa = PessoaMap.ConverterParaEntidade(requisicao, cadastro);

        pessoa.Domicilios = await PessoaMap.RecuperarEndereco(requisicao, _cepService);

        await _repositorioWriteCadastro.Registrar(cadastro);
        await _repositorioWritePessoa.Registrar(pessoa);

        await _unidadeDeTrabalho.Commit();

        return PessoaMap.ConverterParaResposta(pessoa);
    }

    private async Task Validar(RequisicaoPessoaJson requisicao)
    {
        var validator = new RegistrarPessoaValidator();
        var resultado = validator.Validate(requisicao);

        var existePessoaComCpf = await _repositorioReadPessoa.RecuperarPessoaExistentePorCpf(requisicao.Cpf);

        var existePessoaComCnpj = await _repositorioReadPessoa.RecuperarPessoaExistentePorCnpj(requisicao.Cnpj);

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
