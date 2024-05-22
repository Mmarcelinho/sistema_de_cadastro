namespace SistemaDeCadastro.Infrastructure.Migrations.Versoes;

[Migration((long)NumeroVersoes.CriarTabelaPessoa, "Cria tabela pessoa")]
public class Versao0000002 : Migration
{
    public override void Down() { }

    public override void Up()
    {
        var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Pessoas"));

        tabela
              .WithColumn("Cpf").AsString(11).NotNullable()
              .WithColumn("Cnpj").AsString(14).Nullable()
              .WithColumn("Nome").AsString(100).NotNullable()
              .WithColumn("Nascimento").AsDateTime().NotNullable()
              .WithColumn("Token").AsString(255).Nullable()
              .WithColumn("CadastroId").AsInt64().NotNullable().ForeignKey("FK_Pessoa_Cadastro_Id", "Cadastros", "Id");
    }
}
