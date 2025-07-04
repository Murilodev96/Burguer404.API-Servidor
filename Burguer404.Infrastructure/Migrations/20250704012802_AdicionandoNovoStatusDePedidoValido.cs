using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoNovoStatusDePedidoValido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusPedido",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 6, "Aguardando pagamento" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusPedido",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
