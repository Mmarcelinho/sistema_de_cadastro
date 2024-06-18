namespace SistemaDeCadastro.Application.UseCases.Pessoa.Registrar;

public class RegistrarPessoaUseCase : IRegistrarPessoaUseCase
{
    private readonly ICadastroWriteOnlyRepositorio _repositorioCadastro;

    private readonly IPessoaWriteOnlyRepositorio _repositorioWrite;

    private readonly IPessoaReadOnlyRepositorio _repositorioRead;

    private IViaCep _viaCep;

    private IUnidadeDeTrabalho _unidadeDeTrabalho;

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

        var cadastro = new Domain.Entidades.Cadastro
        {
            Email = requisicao.Cadastro.Email,
            NomeFantasia = requisicao.Cadastro.NomeFantasia,
            SobrenomeSocial = requisicao.Cadastro.SobrenomeSocial,
            Empresa = requisicao.Cadastro.Empresa,
            Credencial = new Credencial
            {
                Bloqueada = requisicao.Cadastro.Credencial.Bloqueada,
                Expirada = requisicao.Cadastro.Credencial.Expirada,
                Senha = requisicao.Cadastro.Credencial.Senha
            },
            Inscrito = new Inscrito
            {
                Assinante = requisicao.Cadastro.Inscrito.Assinante,
                Associado = requisicao.Cadastro.Inscrito.Associado,
                Senha = requisicao.Cadastro.Inscrito.Senha
            },
            Parceiro = new Parceiro
            {
                Cliente = requisicao.Cadastro.Parceiro.Cliente,
                Fornecedor = requisicao.Cadastro.Parceiro.Fornecedor,
                Prestador = requisicao.Cadastro.Parceiro.Prestador,
                Colaborador = requisicao.Cadastro.Parceiro.Colaborador
            },
            Documento = new Documento(
               requisicao.Cadastro.Documento.Numero,
               requisicao.Cadastro.Documento.OrgaoEmissor,
               requisicao.Cadastro.Documento.EstadoEmissor,
               requisicao.Cadastro.Documento.DataValidade),
            Identificador = new Identificacao(
               requisicao.Cadastro.Identificador.Empresa,
               requisicao.Cadastro.Identificador.Identificador,
               (IdentificacaoTipo)requisicao.Cadastro.Identificador.Tipo)
        };

        var pessoa = new Domain.Entidades.Pessoa
        {
            Cpf = requisicao.Cpf,
            Cnpj = requisicao.Cnpj,
            Nome = requisicao.Nome,
            NomeFantasia = requisicao.NomeFantasia,
            Email = requisicao.Email,
            Nascimento = requisicao.Nascimento,
            Token = requisicao.Token,
            CadastroId = cadastro.Id,
            Cadastro = cadastro,
            Domicilios = await CepServices(requisicao),
            Telefone = new Telefone(
                requisicao.Telefone.Numero,
                requisicao.Telefone.Celular,
                requisicao.Telefone.Whatsapp,
                requisicao.Telefone.Telegram)
        };

        await _repositorioCadastro.Registrar(cadastro);
        await _repositorioWrite.Registrar(pessoa);

        await _unidadeDeTrabalho.Commit();

        return new RespostaPessoaJson(
            pessoa.Id,
            pessoa.Cpf,
            pessoa.Cnpj,
            pessoa.Nome,
            pessoa.NomeFantasia,
            pessoa.Email,
            pessoa.Nascimento,
            pessoa.Token,
            pessoa.Domicilios.Select(d => new RespostaDomicilioJson(
                (Communication.Enum.DomicilioTipo)d.Tipo,
                new RespostaEnderecoJson(
                    d.Endereco.Cep,
                    d.Endereco.Logradouro,
                    d.Endereco.Numero,
                    d.Endereco.Bairro,
                    d.Endereco.Complemento,
                    d.Endereco.PontoReferencia,
                    d.Endereco.Uf,
                    d.Endereco.Cidade,
                    d.Endereco.Ibge
                )
            )).ToList(),
            new RespostaTelefoneJson(
                pessoa.Telefone.Numero,
                pessoa.Telefone.Celular,
                pessoa.Telefone.Whatsapp,
                pessoa.Telefone.Telegram
            ),
            new RespostaCadastroJson(
                pessoa.Cadastro.Id.ToString(),
                pessoa.Cadastro.DataCriacao.ToString(),
                pessoa.Cadastro.Email,
                pessoa.Cadastro.NomeFantasia,
                pessoa.Cadastro.SobrenomeSocial,
                pessoa.Cadastro.Empresa,
                new RespostaCredencialJson(
                    pessoa.Cadastro.Credencial.Bloqueada,
                    pessoa.Cadastro.Credencial.Expirada,
                    pessoa.Cadastro.Credencial.Senha
                ),
                new RespostaInscritoJson(
                    pessoa.Cadastro.Inscrito.Assinante,
                    pessoa.Cadastro.Inscrito.Associado,
                    pessoa.Cadastro.Inscrito.Senha
                ),
                new RespostaParceiroJson(
                    pessoa.Cadastro.Parceiro.Cliente,
                    pessoa.Cadastro.Parceiro.Fornecedor,
                    pessoa.Cadastro.Parceiro.Prestador,
                    pessoa.Cadastro.Parceiro.Colaborador
                ),
                new RespostaDocumentoJson(
                    pessoa.Cadastro.Documento.Numero,
                    pessoa.Cadastro.Documento.OrgaoEmissor,
                    pessoa.Cadastro.Documento.EstadoEmissor,
                    pessoa.Cadastro.Documento.DataValidade.ToShortDateString()
                ),
                new RespostaIdentificacaoJson(
                    pessoa.Cadastro.Identificador.Empresa,
                    pessoa.Cadastro.Identificador.Identificador,
                    (Communication.Enum.IdentificacaoTipo)pessoa.Cadastro.Identificador.Tipo
                )
            )
        );

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
