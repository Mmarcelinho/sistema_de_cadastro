namespace SistemaDeCadastro.Application.UseCases.Pessoa.Atualizar;

public class AtualizarPessoaUseCase(IPessoaUpdateOnlyRepositorio repositorioUpdatePessoa, IPessoaReadOnlyRepositorio repositorioReadPessoa, ICadastroReadOnlyRepositorio repositorioReadCadastro, IUnidadeDeTrabalho unidadeDeTrabalho, ICepService cepService) : IAtualizarPessoaUseCase
{
    public async Task Executar(long pessoaId, RequisicaoPessoaJson requisicao)
    {
        await Validar(requisicao);

        var pessoa = await repositorioUpdatePessoa.RecuperarPorId(pessoaId);

        if (pessoa is null)
            throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

        pessoa = pessoa.Atualizar(requisicao);

        pessoa.Domicilios = await PessoaMap.RecuperarEndereco(requisicao, cepService);

        repositorioUpdatePessoa.Atualizar(pessoa);

        await unidadeDeTrabalho.Commit();
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

