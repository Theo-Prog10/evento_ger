using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventos_ger.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginAndSenhaToPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Participantes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Participantes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Palestrantes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Palestrantes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Organizadores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Organizadores",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Palestrantes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Palestrantes");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Organizadores");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Organizadores");
        }
    }
}
