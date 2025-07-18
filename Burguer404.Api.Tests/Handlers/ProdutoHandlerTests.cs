using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Controllers;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Produto;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Burguer404.Api.Tests.Handlers
{
    public class ProdutoHandlerTests
    {
        private readonly Mock<IProdutoGateway> _produtoGatewayMock;
        private readonly Mock<ProdutoController> _produtoControllerMock;
        private readonly ProdutoHandler _handler;

        public ProdutoHandlerTests()
        {
            _produtoGatewayMock = new Mock<IProdutoGateway>();
            _produtoControllerMock = new Mock<ProdutoController>(_produtoGatewayMock.Object);
            _handler = new ProdutoHandler(_produtoGatewayMock.Object);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarOk()
        {
            var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            var content = new byte[256];
            new Random().NextBytes(content);
            var stream = new MemoryStream(content);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.FileName).Returns($"imagem_{Guid.NewGuid()}.jpg");
            fileMock.Setup(f => f.Length).Returns(content.Length);
            fileMock.Setup(f => f.ContentType).Returns("image/jpeg");
            var request = new ProdutoRequest {
                Id = 1,
                Nome = "Produto Teste",
                Descricao = "Descricao Teste",
                Preco = 10.0,
                CategoriaProdutoId = 1,
                Imagem = fileMock.Object
            };
            var produtoResponse = new ProdutoResponse { Id = 1, Nome = "Produto Teste", Descricao = "Descricao Teste", Preco = 10.0, CategoriaProdutoId = 1 };
            var response = new ResponseBase<ProdutoResponse>() { Sucesso = true, Resultado = new List<ProdutoResponse> { produtoResponse } };
            // Não é possível mockar métodos não-virtuais do controller
            // _produtoControllerMock.Setup(s => s.CadastrarProduto(It.IsAny<ProdutoRequest>())).ReturnsAsync(response);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.CadastrarProduto(request);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
            // Não é possível mockar métodos não-virtuais do controller
            // _produtoControllerMock.Setup(s => s.CadastrarProduto(request)).ThrowsAsync(new Exception("Erro ao cadastrar"));
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            // Simule erro no repositório se necessário
            var result = await handler.CadastrarProduto(request);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarJsonResult()
        {
            var produtoEntity = new ProdutoEntity { Id = 1, Nome = "Produto Teste", Descricao = "Descricao Teste", Preco = 10.0, CategoriaProdutoId = 1 };
            _produtoGatewayMock.Setup(r => r.ListarProdutosAsync()).ReturnsAsync(new List<ProdutoEntity> { produtoEntity });
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ListarProdutos();
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _produtoGatewayMock.Setup(r => r.ListarProdutosAsync()).ThrowsAsync(new Exception("Erro ao listar"));
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ListarProdutos();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Preco = 10.0,
                CategoriaProdutoId = 1
            };
            var produtoEntity = new ProdutoEntity
            {
                Id = 1,
                Nome = request.Nome,
                Descricao = request.Descricao,
                Preco = request.Preco,
                CategoriaProdutoId = request.CategoriaProdutoId
            };
            _produtoGatewayMock.Setup(r => r.AtualizarProdutoAsync(It.IsAny<ProdutoEntity>())).ReturnsAsync(produtoEntity);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.AtualizarProduto(request);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest
            {
                Nome = "Produto Teste",
                Descricao = "Descrição Teste",
                Preco = 10.0,
                CategoriaProdutoId = 1
            };
            // Não é possível mockar métodos não-virtuais do controller
            // _produtoControllerMock.Setup(s => s.AtualizarProduto(request)).ThrowsAsync(new Exception("Erro ao atualizar"));
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            // Simule erro no repositório se necessário
            var result = await handler.AtualizarProduto(request);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarOk()
        {
            int id = 1;
            var produto = new ProdutoEntity { Id = id };
            _produtoGatewayMock.Setup(r => r.ObterProdutoPorIdAsync(id)).ReturnsAsync(produto);
            _produtoGatewayMock.Setup(r => r.RemoverProdutoAsync(produto)).Returns(Task.CompletedTask);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.RemoverProduto(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            // Case 1: Produto não encontrado
            _produtoGatewayMock.Setup(r => r.ObterProdutoPorIdAsync(id)).ReturnsAsync((ProdutoEntity)null);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.RemoverProduto(id);
            Assert.IsType<BadRequestObjectResult>(result);

            // Case 2: Exception thrown during removal
            var produto = new ProdutoEntity { Id = id };
            _produtoGatewayMock.Setup(r => r.ObterProdutoPorIdAsync(id)).ReturnsAsync(produto);
            _produtoGatewayMock.Setup(r => r.RemoverProdutoAsync(produto)).ThrowsAsync(new Exception("Erro ao remover"));
            handler = new ProdutoHandler(_produtoGatewayMock.Object);
            result = await handler.RemoverProduto(id);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarOk()
        {
            var produtos = new List<ProdutoEntity>
            {
                new ProdutoEntity { Id = 1, Nome = "Lanche Teste", Preco = 10.0, CategoriaProdutoId = 1 },
                new ProdutoEntity { Id = 2, Nome = "Acompanhamento Teste", Preco = 5.0, CategoriaProdutoId = 2 },
                new ProdutoEntity { Id = 3, Nome = "Bebida Teste", Preco = 7.0, CategoriaProdutoId = 3 },
                new ProdutoEntity { Id = 4, Nome = "Sobremesa Teste", Preco = 8.0, CategoriaProdutoId = 4 }
            };
            _produtoGatewayMock.Setup(r => r.ListarProdutosAsync()).ReturnsAsync(produtos);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ObterCardapio();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _produtoGatewayMock.Setup(r => r.ListarProdutosAsync()).ThrowsAsync(new Exception("Erro ao obter cardápio"));
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ObterCardapio();
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarOk()
        {
            int id = 1;
            var produto = new ProdutoEntity { Id = id, ImagemByte = new byte[] { 1, 2, 3 } };
            _produtoGatewayMock.Setup(r => r.ObterProdutoPorIdAsync(id)).ReturnsAsync(produto);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.VisualizarImagem(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            // Case 1: Produto não encontrado (repository returns null)
            _produtoGatewayMock.Setup(r => r.VisualizarImagemAsync(id)).ReturnsAsync((ProdutoEntity)null);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.VisualizarImagem(id);
            Assert.IsType<BadRequestObjectResult>(result);

            // Case 2: Exception thrown during repository call
            _produtoGatewayMock.Setup(r => r.VisualizarImagemAsync(id)).ThrowsAsync(new Exception("Erro ao visualizar imagem"));
            handler = new ProdutoHandler(_produtoGatewayMock.Object);
            result = await handler.VisualizarImagem(id);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarOk()
        {
            int categoriaId = 1;
            var produtos = new List<ProdutoEntity> { new ProdutoEntity { Id = 1, Nome = "Produto Teste", CategoriaProdutoId = categoriaId } };
            _produtoGatewayMock.Setup(r => r.ObterProdutosPorCategoriaIdAsync(categoriaId)).ReturnsAsync(produtos);
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ObterProdutosPorCategoria(categoriaId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int categoriaId = 1;
            _produtoGatewayMock.Setup(r => r.ObterProdutosPorCategoriaIdAsync(categoriaId)).ThrowsAsync(new Exception("Erro ao obter produtos por categoria"));
            var controller = new ProdutoController(_produtoGatewayMock.Object);
            var handler = new ProdutoHandler(_produtoGatewayMock.Object);
            var result = await handler.ObterProdutosPorCategoria(categoriaId);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
