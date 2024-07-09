using SistemaDeCadastro.Application.Mappings;

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

        pessoa = pessoa.Atualizar(requisicao);

        pessoa.Domicilios = await CepServices(requisicao);

        _repositorioUpdate.Atualizar(pessoa);

        await _unidadeDeTrabalho.Commit();
    }

    private async Task Validar(RequisicaoPessoaJson requisicao)
    {
        var validatorPessoa = new RegistrarPessoaValidator();
        var validatorCadastro = new RegistrarCadastroValidator();
        var resultado = validatorPessoa.Validate(requisicao);
        resultado = validatorCadastro.Validate(requisicao.Cadastro);

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

