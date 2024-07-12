namespace SistemaDeCadastro.Application.UseCases.Pessoa.Atualizar;

public class AtualizarPessoaUseCase : IAtualizarPessoaUseCase
{
    private readonly IPessoaUpdateOnlyRepositorio _repositorioUpdate;

    private readonly IPessoaReadOnlyRepositorio _repositorioRead;

    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    private readonly IViaCep _viaCep;

    public AtualizarPessoaUseCase(IPessoaUpdateOnlyRepositorio repositorioUpdate, IPessoaReadOnlyRepositorio repositorioRead, IUnidadeDeTrabalho unidadeDeTrabalho, IViaCep viaCep)
    {
        _repositorioUpdate = repositorioUpdate;
        _repositorioRead = repositorioRead;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _viaCep = viaCep;
    }

    public async Task Executar(long pessoaId, RequisicaoPessoaJson requisicao)
    {
        await Validar(requisicao);

        var pessoa = await _repositorioUpdate.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        pessoa = pessoa.Atualizar(requisicao);

        pessoa.Domicilios = await PessoaMap.RecuperarEndereco(requisicao, _viaCep);

        _repositorioUpdate.Atualizar(pessoa);

        await _unidadeDeTrabalho.Commit();
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
}

