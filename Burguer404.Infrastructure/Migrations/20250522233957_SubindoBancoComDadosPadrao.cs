using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Burguer404.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubindoBancoComDadosPadrao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "CodPedidoSequence",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "CategoriaPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaPedido", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "StatusPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    CategoriaPedidoId = table.Column<int>(type: "int", nullable: false),
                    ImagemByte = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_CategoriaPedido_CategoriaPedidoId",
                        column: x => x.CategoriaPedidoId,
                        principalTable: "CategoriaPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false),
                    Cpf = table.Column<string>(type: "nchar(14)", fixedLength: true, maxLength: 14, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PerfilClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_PerfilCliente_PerfilClienteId",
                        column: x => x.PerfilClienteId,
                        principalTable: "PerfilCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPedido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusPedidoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_StatusPedido_StatusPedidoId",
                        column: x => x.StatusPedidoId,
                        principalTable: "StatusPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriaPedido",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Lanche" },
                    { 2, "Acompanhamento" },
                    { 3, "Bebida" },
                    { 4, "Sobremesa" }
                });

            migrationBuilder.InsertData(
                table: "PerfilCliente",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "usuario" }
                });

            migrationBuilder.InsertData(
                table: "StatusPedido",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Recebido" },
                    { 2, "Em preparação" },
                    { 3, "Pronto" },
                    { 4, "Finalizado" },
                    { 5, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Cpf", "Email", "Nome", "PerfilClienteId", "Status" },
                values: new object[,]
                {
                    { 1, "111.111.111-11", "admin@admin.com", "admin", 1, true },
                    { 2, "123.456.789-10", "usuario@usuario.com", "usuario", 2, true }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaPedidoId", "Descricao", "ImagemByte", "Nome", "Preco", "Status" },
                values: new object[,]
                {
                    { 1, 1, "adicional de bacon", null, "X-Bacon", 31.989999999999998, true },
                    { 2, 3, "Zero açucar", null, "Coca-Cola", 7.0, true },
                    { 3, 2, "300g", null, "Batata frita", 15.0, true },
                    { 4, 4, "Morango", null, "Sorvete", 9.0, true },
                    { 5, 1, "saladinha da boa", null, "X-Salada", 24.989999999999998, true },
                    { 6, 3, "concorrente", null, "Pepsi", 7.0, true },
                    { 7, 2, "300g", null, "Onion rings", 20.0, true },
                    { 8, 4, "Chocolate com morango", null, "Bolo de pote", 14.0, true },
                    { 9, 1, "tudo do bom e do melhor", null, "X-Tudo", 40.0, true },
                    { 10, 3, "suquinho", null, "Suco de maracuja", 10.0, true },
                    { 11, 2, "400g", null, "Batata + Onion rings P", 27.5, true },
                    { 12, 4, "Melhor de todos", null, "Pudim", 99.0, true },
                    { 13, 1, "fitness", null, "X-Frango", 22.989999999999998, true },
                    { 14, 1, "pouca gordura graças a Deus", null, "X-Calabresa", 26.989999999999998, true },
                    { 15, 1, "suculência ao máximo", null, "X-Picanha", 36.990000000000002, true },
                    { 16, 3, "suquinho 2", null, "Suco de limão", 7.0, true },
                    { 17, 3, "água de torneira", null, "H2O", 5.0, true },
                    { 18, 2, "700g", null, "Batata + Onion rings M", 33.0, true },
                    { 19, 2, "1Kg", null, "Batata + Onion rings G", 41.0, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Cpf",
                table: "Clientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PerfilClienteId",
                table: "Clientes",
                column: "PerfilClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_PedidoId",
                table: "PedidoProduto",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_ProdutoId",
                table: "PedidoProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_StatusPedidoId",
                table: "Pedidos",
                column: "StatusPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaPedidoId",
                table: "Produtos",
                column: "CategoriaPedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProduto");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "StatusPedido");

            migrationBuilder.DropTable(
                name: "CategoriaPedido");

            migrationBuilder.DropTable(
                name: "PerfilCliente");

            migrationBuilder.DropSequence(
                name: "CodPedidoSequence",
                schema: "dbo");
        }
    }
}
