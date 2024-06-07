using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeCadastro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version0000001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SobrenomeSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Empresa = table.Column<bool>(type: "bit", nullable: false),
                    CredencialBloqueada = table.Column<bool>(type: "bit", nullable: true),
                    CredencialExpirada = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CredencialSenha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InscritoAssinante = table.Column<bool>(type: "bit", nullable: true),
                    InscritoAssociado = table.Column<bool>(type: "bit", nullable: true),
                    InscritoSenha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ParceiroCliente = table.Column<bool>(type: "bit", nullable: true),
                    ParceiroFornecedor = table.Column<bool>(type: "bit", nullable: true),
                    ParceiroPrestador = table.Column<bool>(type: "bit", nullable: true),
                    ParceiroColaborador = table.Column<bool>(type: "bit", nullable: true),
                    DocumentoNumero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentoOrgaoEmissor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentoEstadoEmissor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentoDataValidade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdentificacaoEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdentificacaoIdentificador = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdentificacaoTipo = table.Column<int>(type: "int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    TelefoneNumero = table.Column<long>(type: "bigint", nullable: true),
                    TelefoneCelular = table.Column<bool>(type: "bit", nullable: true),
                    TelefoneWhatsapp = table.Column<bool>(type: "bit", nullable: true),
                    TelefoneTelegram = table.Column<bool>(type: "bit", nullable: true),
                    CadastroId = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Domicilios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomicilioTipo = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PontoReferencia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ibge = table.Column<int>(type: "int", nullable: true),
                    PessoaId = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Domicilios_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domicilios_PessoaId",
                table: "Domicilios",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CadastroId",
                table: "Pessoas",
                column: "CadastroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domicilios");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Cadastros");
        }
    }
}
