using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoSequenceParaClienteAnonimo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "CodAnonimoSequence",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "CodAnonimoSequence",
                schema: "dbo");
        }
    }
}
