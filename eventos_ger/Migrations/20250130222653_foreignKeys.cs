using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventos_ger.Migrations
{
    /// <inheritdoc />
    public partial class foreignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizadorId",
                table: "Eventos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_id_usuario",
                table: "Pessoas",
                column: "id_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_id_local",
                table: "Eventos",
                column: "id_local");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_OrganizadorId",
                table: "Eventos",
                column: "OrganizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Associacoes_idEvento",
                table: "Associacoes",
                column: "idEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Associacoes_idPessoa",
                table: "Associacoes",
                column: "idPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Associacoes_Eventos_idEvento",
                table: "Associacoes",
                column: "idEvento",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Associacoes_Pessoas_idPessoa",
                table: "Associacoes",
                column: "idPessoa",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Locais_id_local",
                table: "Eventos",
                column: "id_local",
                principalTable: "Locais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Pessoas_OrganizadorId",
                table: "Eventos",
                column: "OrganizadorId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Usuarios_id_usuario",
                table: "Pessoas",
                column: "id_usuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associacoes_Eventos_idEvento",
                table: "Associacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Associacoes_Pessoas_idPessoa",
                table: "Associacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Locais_id_local",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Pessoas_OrganizadorId",
                table: "Eventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Usuarios_id_usuario",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_id_usuario",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_id_local",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_OrganizadorId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Associacoes_idEvento",
                table: "Associacoes");

            migrationBuilder.DropIndex(
                name: "IX_Associacoes_idPessoa",
                table: "Associacoes");

            migrationBuilder.DropColumn(
                name: "OrganizadorId",
                table: "Eventos");
        }
    }
}
