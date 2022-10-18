using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_Api.Migrations
{
    public partial class passwordhash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tipos_Nome",
                table: "Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Tipos_Sigla",
                table: "Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Concessoes_Nome",
                table: "Concessoes");

            migrationBuilder.DropIndex(
                name: "IX_Concessoes_Sigla",
                table: "Concessoes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "longblob",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Nome",
                table: "Tipos",
                column: "Nome",
                unique: true)
                .Annotation("MySql:IndexPrefixLength", new[] { 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Sigla",
                table: "Tipos",
                column: "Sigla",
                unique: true)
                .Annotation("MySql:IndexPrefixLength", new[] { 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_Descricao",
                table: "Documentos",
                column: "Descricao")
                .Annotation("MySql:IndexPrefixLength", new[] { 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Concessoes_Nome",
                table: "Concessoes",
                column: "Nome",
                unique: true)
                .Annotation("MySql:IndexPrefixLength", new[] { 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Concessoes_Sigla",
                table: "Concessoes",
                column: "Sigla",
                unique: true)
                .Annotation("MySql:IndexPrefixLength", new[] { 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tipos_Nome",
                table: "Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Tipos_Sigla",
                table: "Tipos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_Descricao",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Concessoes_Nome",
                table: "Concessoes");

            migrationBuilder.DropIndex(
                name: "IX_Concessoes_Sigla",
                table: "Concessoes");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Nome",
                table: "Tipos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Sigla",
                table: "Tipos",
                column: "Sigla",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concessoes_Nome",
                table: "Concessoes",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concessoes_Sigla",
                table: "Concessoes",
                column: "Sigla",
                unique: true);
        }
    }
}
