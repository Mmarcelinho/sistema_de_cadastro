namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaUseCase : IRegistrarPessoaUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioCadastro;

    private readonly IPessoaWriteOnlyRepositorio _repositorioWrite;

    private readonly IPessoaReadOnlyRepositorio _repositorioRead;

    private readonly IViaCep _viaCep;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RegistrarPessoaUseCase(ICadastroWriteOnlyRepositorio repositorioCadastro, IPessoaReadOnlyRepositorio repositorioRead, IPessoaWriteOnlyRepositorio repositorioWrite, IViaCep viaCep, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorioCadastro = repositorioCadastro;
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

        pessoa.Domicilios = await CepServices(requisicao);

        await _repositorioCadastro.Registrar(cadastro);
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

        if (existePessoaComCpf)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", PessoaMensagensDeErro.PESSOA_CPF_JA_REGISTRADO));

        else if (existePessoaComCnpj)
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", PessoaMensagensDeErro.PESSOA_CNPJ_JA_REGISTRADO));

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }

    private async Task<List<Domicilio>> CepServices(RequisicaoPessoaJson requisicao)
    {
        List<Domicilio> domicilios = [];

        foreach (var domicilio in requisicao.Domicilios)
        {
            var resultado = await _viaCep.RecuperarEndereco(domicilio.Endereco.Cep);

            Endereco Endereco = new(
                domicilio.Endereco.Cep,
                resultado.Logradouro,
                domicilio.Endereco.Numero,
                resultado.Bairro,
                domicilio.Endereco.Complemento,
                domicilio.Endereco.PontoReferencia,
                resultado.Uf,
                resultado.Localidade,
                resultado.Ibge);

            Domicilio Domicilio = new()
            {
                Tipo = (DomicilioTipo)domicilio.Tipo,
                Endereco = Endereco
            };

            domicilios.Add(Domicilio);
        }
        return domicilios;
    }
}
