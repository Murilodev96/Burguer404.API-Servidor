using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoPerfilCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerfilClienteId",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PerfilCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilCliente", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PerfilCliente",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "usuario" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PerfilClienteId",
                table: "Clientes",
                column: "PerfilClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_PerfilCliente_PerfilClienteId",
                table: "Clientes",
                column: "PerfilClienteId",
                principalTable: "PerfilCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_PerfilCliente_PerfilClienteId",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "PerfilCliente");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_PerfilClienteId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PerfilClienteId",
                table: "Clientes");
        }
    }
}
