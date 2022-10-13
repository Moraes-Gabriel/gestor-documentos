using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_Api.Migrations
{
    public partial class changing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Documentos",
                keyColumn: "urlArquivoS3",
                keyValue: null,
                column: "urlArquivoS3",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "urlArquivoS3",
                table: "Documentos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "urlArquivoS3",
                table: "Documentos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
