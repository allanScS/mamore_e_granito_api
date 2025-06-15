using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmoreGranito.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSenhaHashColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senha_hash",
                table: "usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "senha_hash",
                table: "usuarios",
                type: "text",
                nullable: true);
        }
    }
}
