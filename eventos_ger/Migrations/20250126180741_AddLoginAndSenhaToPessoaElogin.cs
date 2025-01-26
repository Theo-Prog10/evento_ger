using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventos_ger.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginAndSenhaToPessoaElogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login_pessoa",
                table: "Associacoes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login_pessoa",
                table: "Associacoes");
        }
    }
}
