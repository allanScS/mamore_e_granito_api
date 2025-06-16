using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmoreGranito.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStatusAndDataConclusaoFromProcessoSerragem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_conclusao",
                table: "processos_serragem");

            migrationBuilder.DropColumn(
                name: "status",
                table: "processos_serragem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "data_conclusao",
                table: "processos_serragem",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "processos_serragem",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
