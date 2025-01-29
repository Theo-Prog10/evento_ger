using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eventos_ger.Migrations
{
    /// <inheritdoc />
    public partial class refatoracaoPessoaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizadores");

            migrationBuilder.DropTable(
                name: "Palestrantes");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropColumn(
                name: "login_pessoa",
                table: "Associacoes");

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: true),
                    nascimento = table.Column<DateOnly>(type: "date", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    biografia = table.Column<string>(type: "text", nullable: true),
                    especialidade = table.Column<string>(type: "text", nullable: true),
                    contato = table.Column<string>(type: "text", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: true),
                    senha = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "login_pessoa",
                table: "Associacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    contato = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    nascimento = table.Column<string>(type: "text", nullable: true),
                    nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    biografia = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    especialidade = table.Column<string>(type: "text", nullable: true),
                    nascimento = table.Column<string>(type: "text", nullable: true),
                    nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<string>(type: "text", nullable: true),
                    nascimento = table.Column<string>(type: "text", nullable: true),
                    nome = table.Column<string>(type: "text", nullable: true),
                    status_inscricao = table.Column<string>(type: "text", nullable: true),
                    tipo_ingresso = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });
        }
    }
}
