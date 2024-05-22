namespace SistemaDeCadastro.Infrastructure.Migrations.Versoes;

[Migration((long)NumeroVersoes.CriarTabelaCadastro, "Cria tabela cadastro")]
public class Versao0000001 : Migration
{
    public override void Down() { }

    public override void Up()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Cadastros"));

        tabela
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("NomeFantasia").AsString(100).NotNullable()
                .WithColumn("SobrenomeSocial").AsString(100).NotNullable()
                .WithColumn("Empresa").AsBoolean().NotNullable()
                .WithColumn("CredencialBloqueada").AsBoolean().NotNullable()
                .WithColumn("CredencialExpirada").AsString(255).NotNullable()
                .WithColumn("CredencialSenha").AsString(255).NotNullable()
                .WithColumn("InscritoAssinante").AsBoolean().NotNullable()
                .WithColumn("InscritoAssociado").AsBoolean().NotNullable()
                .WithColumn("InscritoSenha").AsString(255).NotNullable()
                .WithColumn("ParceiroCliente").AsBoolean().NotNullable()
                .WithColumn("ParceiroFornecedor").AsBoolean().NotNullable()
                .WithColumn("ParceiroPrestador").AsBoolean().NotNullable()
                .WithColumn("ParceiroColaborador").AsBoolean().NotNullable()
                .WithColumn("DocumentoNumero").AsString(50).NotNullable()
                .WithColumn("DocumentoOrgaoEmissor").AsString(100).NotNullable()
                .WithColumn("DocumentoEstadoEmissor").AsString(50).NotNullable()
                .WithColumn("DocumentoDataValidade").AsDateTime().NotNullable()
                .WithColumn("IdentificacaoEmpresa").AsInt32().NotNullable()
                .WithColumn("IdentificacaoIdentificador").AsString(255).NotNullable()
                .WithColumn("IdentificacaoTipo").AsInt16().NotNullable(); 
    }
}

