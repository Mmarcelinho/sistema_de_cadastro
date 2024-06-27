namespace SistemaDeCadastro.Application.UseCases.Pessoa.RecuperarPorId;

public class RecuperarPessoaPorIdUseCase : IRecuperarPessoaPorIdUseCase
{
    private readonly IPessoaReadOnlyRepositorio _repositorio;
    private readonly ICachingService _cache;

    public RecuperarPessoaPorIdUseCase(IPessoaReadOnlyRepositorio repositorio, ICachingService cache)
    {
        _repositorio = repositorio;
        _cache = cache;
    }


    public async Task<RespostaPessoaJson> Executar(long pessoaId)
    {
        string? pessoaCache = await _cache.Recuperar(pessoaId.ToString());
        Domain.Entidades.Pessoa? pessoa;

        if (string.IsNullOrEmpty(pessoaCache))
        {
            pessoa = await _repositorio.RecuperarPorId(pessoaId);

            if (pessoa is null)
                throw new NaoEncontradoException(PessoaMensagensDeErro.PESSOA_NAO_ENCONTRADO);

            await _cache.Registrar(pessoaId.ToString(), JsonConvert.SerializeObject(pessoa));

            return MapearPessoaParaResposta(pessoa);
        }

        pessoa = JsonConvert.DeserializeObject<Domain.Entidades.Pessoa>(pessoaCache);

        return MapearPessoaParaResposta(pessoa);
    }

    private static RespostaPessoaJson MapearPessoaParaResposta(Domain.Entidades.Pessoa pessoa)
    {
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
                pessoa.Cadastro.Id,
                pessoa.Cadastro.DataCriacao,
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
                    pessoa.Cadastro.Documento.DataValidade
                ),
                new RespostaIdentificacaoJson(
                    pessoa.Cadastro.Identificador.Empresa,
                    pessoa.Cadastro.Identificador.Identificador,
                    (Communication.Enum.IdentificacaoTipo)pessoa.Cadastro.Identificador.Tipo
                )
            )
        );
    }
}
