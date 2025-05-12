using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alterandocolunaimagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Produtos");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagemByte",
                table: "Produtos",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemByte",
                table: "Produtos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Produtos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
