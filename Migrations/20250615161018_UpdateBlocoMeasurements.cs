using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmoreGranito.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlocoMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processo_serragem_blocos_bloco_id",
                table: "processo_serragem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processo_serragem",
                table: "processo_serragem");

            migrationBuilder.RenameTable(
                name: "processo_serragem",
                newName: "processos_serragem");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "usuarios",
                newName: "cargo");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "usuarios",
                newName: "data_cadastro");

            migrationBuilder.RenameColumn(
                name: "metragem_m3",
                table: "blocos",
                newName: "largura");

            migrationBuilder.RenameIndex(
                name: "IX_processo_serragem_bloco_id",
                table: "processos_serragem",
                newName: "IX_processos_serragem_bloco_id");

            migrationBuilder.AddColumn<decimal>(
                name: "altura",
                table: "blocos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "comprimento",
                table: "blocos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "processos_serragem",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observacoes",
                table: "processos_serragem",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_processos_serragem",
                table: "processos_serragem",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_processos_serragem_blocos_bloco_id",
                table: "processos_serragem",
                column: "bloco_id",
                principalTable: "blocos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_processos_serragem_blocos_bloco_id",
                table: "processos_serragem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_processos_serragem",
                table: "processos_serragem");

            migrationBuilder.DropColumn(
                name: "altura",
                table: "blocos");

            migrationBuilder.DropColumn(
                name: "comprimento",
                table: "blocos");

            migrationBuilder.RenameTable(
                name: "processos_serragem",
                newName: "processo_serragem");

            migrationBuilder.RenameColumn(
                name: "cargo",
                table: "usuarios",
                newName: "Cargo");

            migrationBuilder.RenameColumn(
                name: "data_cadastro",
                table: "usuarios",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "largura",
                table: "blocos",
                newName: "metragem_m3");

            migrationBuilder.RenameIndex(
                name: "IX_processos_serragem_bloco_id",
                table: "processo_serragem",
                newName: "IX_processo_serragem_bloco_id");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "processo_serragem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "observacoes",
                table: "processo_serragem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_processo_serragem",
                table: "processo_serragem",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_processo_serragem_blocos_bloco_id",
                table: "processo_serragem",
                column: "bloco_id",
                principalTable: "blocos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
