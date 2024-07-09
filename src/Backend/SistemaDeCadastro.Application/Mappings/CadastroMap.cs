namespace SistemaDeCadastro.Application.Mappings;

public static class CadastroMap
{
    public static Cadastro Atualizar(this Cadastro cadastro, RequisicaoCadastroJson requisicao)
    {
        cadastro.Email = requisicao.Email;
        cadastro.SobrenomeSocial = requisicao.SobrenomeSocial;
        cadastro.Empresa = requisicao.Empresa;
        cadastro.Credencial = new Credencial
        {
            Bloqueada = requisicao.Credencial.Bloqueada,
            Expirada = requisicao.Credencial.Expirada,
            Senha = requisicao.Credencial.Senha,
        };
        cadastro.Inscrito = new Inscrito
        {
            Assinante = requisicao.Inscrito.Assinante,
            Associado = requisicao.Inscrito.Associado,
            Senha = requisicao.Inscrito.Senha
        };
        cadastro.Parceiro = new Parceiro
        {
            Cliente = requisicao.Parceiro.Cliente,
            Fornecedor = requisicao.Parceiro.Fornecedor,
            Prestador = requisicao.Parceiro.Prestador,
            Colaborador = requisicao.Parceiro.Colaborador
        };
        cadastro.Documento = new Documento(
                    requisicao.Documento.Numero,
                    requisicao.Documento.OrgaoEmissor,
                    requisicao.Documento.EstadoEmissor,
                    requisicao.Documento.DataValidade
                );
        cadastro.Identificador = new Identificacao(
                    requisicao.Identificador.Empresa,
                    requisicao.Identificador.Identificador,
                    (IdentificacaoTipo)requisicao.Identificador.Tipo
                );
        return cadastro;
    }

    public static Cadastro ConverterParaEntidade(RequisicaoCadastroJson requisicao)
    {
        Cadastro cadastro = new()
        {
            Email = requisicao.Email,
            NomeFantasia = requisicao.NomeFantasia,
            SobrenomeSocial = requisicao.SobrenomeSocial,
            Empresa = requisicao.Empresa,
            Credencial = new Credencial
            {
                Bloqueada = requisicao.Credencial.Bloqueada,
                Expirada = requisicao.Credencial.Expirada,
                Senha = requisicao.Credencial.Senha,
            },
            Inscrito = new Inscrito
            {
                Assinante = requisicao.Inscrito.Assinante,
                Associado = requisicao.Inscrito.Associado,
                Senha = requisicao.Inscrito.Senha
            },
            Parceiro = new Parceiro
            {
                Cliente = requisicao.Parceiro.Cliente,
                Fornecedor = requisicao.Parceiro.Fornecedor,
                Prestador = requisicao.Parceiro.Prestador,
                Colaborador = requisicao.Parceiro.Colaborador
            },
            Documento = new Documento(
                        requisicao.Documento.Numero,
                        requisicao.Documento.OrgaoEmissor,
                        requisicao.Documento.EstadoEmissor,
                        requisicao.Documento.DataValidade
                    ),
            Identificador = new Identificacao(
                        requisicao.Identificador.Empresa,
                        requisicao.Identificador.Identificador,
                        (IdentificacaoTipo)requisicao.Identificador.Tipo
                    )
        };
        return cadastro;
    }
    public static RespostaCadastroJson ConverterParaResposta(this Cadastro cadastro)
    =>
    new RespostaCadastroJson(
        cadastro.Id,
        cadastro.DataCriacao,
        cadastro.Email,
        cadastro.NomeFantasia,
        cadastro.SobrenomeSocial,
        cadastro.Empresa,
        new RespostaCredencialJson(
            cadastro.Credencial.Bloqueada,
            cadastro.Credencial.Expirada,
            cadastro.Credencial.Senha),
        new RespostaInscritoJson(
            cadastro.Inscrito.Assinante,
            cadastro.Inscrito.Associado,
            cadastro.Inscrito.Senha),
        new RespostaParceiroJson(
            cadastro.Parceiro.Cliente,
            cadastro.Parceiro.Fornecedor,
            cadastro.Parceiro.Prestador,
            cadastro.Parceiro.Colaborador),
        new RespostaDocumentoJson(
            cadastro.Documento.Numero,
            cadastro.Documento.OrgaoEmissor,
            cadastro.Documento.EstadoEmissor,
            cadastro.Documento.DataValidade),
        new RespostaIdentificacaoJson(
            cadastro.Identificador.Empresa,
            cadastro.Identificador.Identificador,
            (Communication.Enum.IdentificacaoTipo)cadastro.Identificador.Tipo));
}

