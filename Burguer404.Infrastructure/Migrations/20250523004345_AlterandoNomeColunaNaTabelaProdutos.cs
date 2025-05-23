using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoNomeColunaNaTabelaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_CategoriaPedido_CategoriaPedidoId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "CategoriaPedidoId",
                table: "Produtos",
                newName: "CategoriaProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CategoriaPedidoId",
                table: "Produtos",
                newName: "IX_Produtos_CategoriaProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_CategoriaPedido_CategoriaProdutoId",
                table: "Produtos",
                column: "CategoriaProdutoId",
                principalTable: "CategoriaPedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_CategoriaPedido_CategoriaProdutoId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "CategoriaProdutoId",
                table: "Produtos",
                newName: "CategoriaPedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_CategoriaProdutoId",
                table: "Produtos",
                newName: "IX_Produtos_CategoriaPedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_CategoriaPedido_CategoriaPedidoId",
                table: "Produtos",
                column: "CategoriaPedidoId",
                principalTable: "CategoriaPedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
