namespace SistemaDeCadastro.Application.Mappings;

public static class PessoaMap
{
    public static Pessoa Atualizar(this Pessoa pessoa, RequisicaoPessoaJson requisicao)
    {
        pessoa.Cadastro.Email = requisicao.Cadastro.Email;
        pessoa.Cadastro.NomeFantasia = requisicao.Cadastro.NomeFantasia;
        pessoa.Cadastro.SobrenomeSocial = requisicao.Cadastro.SobrenomeSocial;
        pessoa.Cadastro.Empresa = requisicao.Cadastro.Empresa;
        pessoa.Cadastro.Credencial = new Credencial
        {
            Bloqueada = requisicao.Cadastro.Credencial.Bloqueada,
            Expirada = requisicao.Cadastro.Credencial.Expirada,
            Senha = requisicao.Cadastro.Credencial.Senha
        };
        pessoa.Cadastro.Inscrito = new Inscrito
        {
            Assinante = requisicao.Cadastro.Inscrito.Assinante,
            Associado = requisicao.Cadastro.Inscrito.Associado,
            Senha = requisicao.Cadastro.Inscrito.Senha
        };
        pessoa.Cadastro.Parceiro = new Parceiro
        {
            Cliente = requisicao.Cadastro.Parceiro.Cliente,
            Fornecedor = requisicao.Cadastro.Parceiro.Fornecedor,
            Prestador = requisicao.Cadastro.Parceiro.Prestador,
            Colaborador = requisicao.Cadastro.Parceiro.Colaborador
        };
        pessoa.Cadastro.Documento = new Documento(
           requisicao.Cadastro.Documento.Numero,
           requisicao.Cadastro.Documento.OrgaoEmissor,
           requisicao.Cadastro.Documento.EstadoEmissor,
           requisicao.Cadastro.Documento.DataValidade);
        pessoa.Cadastro.Identificador = new Identificacao(
           requisicao.Cadastro.Identificador.Empresa,
           requisicao.Cadastro.Identificador.Identificador,
           (IdentificacaoTipo)requisicao.Cadastro.Identificador.Tipo);

        pessoa.Cpf = requisicao.Cpf;
        pessoa.Cnpj = requisicao.Cnpj;
        pessoa.Nome = requisicao.Nome;
        pessoa.NomeFantasia = requisicao.NomeFantasia;
        pessoa.Email = requisicao.Email;
        pessoa.Nascimento = requisicao.Nascimento;
        pessoa.Token = requisicao.Token;
        pessoa.Telefone = new Telefone(
            requisicao.Telefone.Numero,
            requisicao.Telefone.Celular,
            requisicao.Telefone.Whatsapp,
            requisicao.Telefone.Telegram);

        return pessoa;
    }

    public static Pessoa ConverterParaEntidade(RequisicaoPessoaJson requisicao, Cadastro cadastro)
    {
        Pessoa pessoa = new()
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
            Telefone = new Telefone(
                requisicao.Telefone.Numero,
                requisicao.Telefone.Celular,
                requisicao.Telefone.Whatsapp,
                requisicao.Telefone.Telegram)
        };
        return pessoa;
    }

    public static RespostaPessoaJson ConverterParaResposta(this Pessoa pessoa)
    =>
    new RespostaPessoaJson(
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
