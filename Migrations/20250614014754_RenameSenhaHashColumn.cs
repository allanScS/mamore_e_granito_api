using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmoreGranito.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameSenhaHashColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "usuarios",
                newName: "senha_hash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senha_hash",
                table: "usuarios",
                newName: "SenhaHash");
        }
    }
}
