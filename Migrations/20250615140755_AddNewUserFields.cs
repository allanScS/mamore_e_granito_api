using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarmoreGranito.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "usuarios",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAtualizacao",
                table: "usuarios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "usuarios",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "DataUltimaAtualizacao",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "usuarios");
        }
    }
}
