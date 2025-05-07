using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTipoDadoSequencePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "CodPedidoSequence",
                schema: "dbo");

            migrationBuilder.CreateSequence(
                name: "CodPedidoSequence",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "CodPedidoSequence",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "CodPedidoSequence",
                schema: "dbo");
        }
    }
}
