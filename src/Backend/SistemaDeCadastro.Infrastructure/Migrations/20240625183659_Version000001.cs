using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaDeCadastro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Version000001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SobrenomeSocial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Empresa = table.Column<bool>(type: "boolean", nullable: false),
                    CredencialBloqueada = table.Column<bool>(type: "boolean", nullable: true),
                    CredencialExpirada = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CredencialSenha = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    InscritoAssinante = table.Column<bool>(type: "boolean", nullable: true),
                    InscritoAssociado = table.Column<bool>(type: "boolean", nullable: true),
                    InscritoSenha = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ParceiroCliente = table.Column<bool>(type: "boolean", nullable: true),
                    ParceiroFornecedor = table.Column<bool>(type: "boolean", nullable: true),
                    ParceiroPrestador = table.Column<bool>(type: "boolean", nullable: true),
                    ParceiroColaborador = table.Column<bool>(type: "boolean", nullable: true),
                    DocumentoNumero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DocumentoOrgaoEmissor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DocumentoEstadoEmissor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DocumentoDataValidade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdentificacaoEmpresa = table.Column<int>(type: "integer", nullable: true),
                    IdentificacaoIdentificador = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IdentificacaoTipo = table.Column<int>(type: "integer", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Token = table.Column<int>(type: "integer", maxLength: 255, nullable: false),
                    TelefoneNumero = table.Column<long>(type: "bigint", nullable: true),
                    TelefoneCelular = table.Column<bool>(type: "boolean", nullable: true),
                    TelefoneWhatsapp = table.Column<bool>(type: "boolean", nullable: true),
                    TelefoneTelegram = table.Column<bool>(type: "boolean", nullable: true),
                    CadastroId = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DomicilioTipo = table.Column<int>(type: "integer", nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Logradouro = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PontoReferencia = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ibge = table.Column<string>(type: "text", nullable: true),
                    PessoaId = table.Column<long>(type: "bigint", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
