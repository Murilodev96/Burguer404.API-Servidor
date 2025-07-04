using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Ports.Repositories.Produto;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Burguer404.Api.Tests.Controllers
{
    public class ProdutoHandlerTests
    {
        private readonly Mock<IRepositoryProduto> _produtoRepoMock;
        private readonly Mock<ProdutoController> _produtoControllerMock;
        private readonly ProdutoHandler _handler;

        public ProdutoHandlerTests()
        {
            _produtoRepoMock = new Mock<IRepositoryProduto>();
            _produtoControllerMock = new Mock<ProdutoController>(_produtoRepoMock.Object);
            _handler = new ProdutoHandler(_produtoRepoMock.Object);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest();
            var response = new ResponseBase<ProdutoResponse>();
            _produtoControllerMock.Setup(s => s.CadastrarProduto(request)).ReturnsAsync(response);

            var result = await _handler.CadastrarProduto(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
            _produtoControllerMock.Setup(s => s.CadastrarProduto(request)).ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _handler.CadastrarProduto(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarJsonResult()
        {
            var response = new ResponseBase<ProdutoResponse>();
            _produtoControllerMock.Setup(s => s.ListarProdutos()).ReturnsAsync(response);

            var result = await _handler.ListarProdutos();

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _produtoControllerMock.Setup(s => s.ListarProdutos()).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _handler.ListarProdutos();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest();
            var response = new ResponseBase<ProdutoResponse>();
            _produtoControllerMock.Setup(s => s.AtualizarProduto(request)).ReturnsAsync(response);

            var result = await _handler.AtualizarProduto(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
            _produtoControllerMock.Setup(s => s.AtualizarProduto(request)).ThrowsAsync(new Exception("Erro ao atualizar"));

            var result = await _handler.AtualizarProduto(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarOk()
        {
            int id = 1;
            var response = new ResponseBase<bool>();
            _produtoControllerMock.Setup(s => s.RemoverProduto(id)).ReturnsAsync(response);

            var result = await _handler.RemoverProduto(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            _produtoControllerMock.Setup(s => s.RemoverProduto(id)).ThrowsAsync(new Exception("Erro ao remover"));

            var result = await _handler.RemoverProduto(id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarOk()
        {
            var response = new ResponseBase<CardapioResponse>();
            _produtoControllerMock.Setup(s => s.ObterCardapio()).ReturnsAsync(response);

            var result = await _handler.ObterCardapio();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _produtoControllerMock.Setup(s => s.ObterCardapio()).ThrowsAsync(new Exception("Erro ao obter cardápio"));

            var result = await _handler.ObterCardapio();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarOk()
        {
            int id = 1;
            var response = new ResponseBase<string>();
            _produtoControllerMock.Setup(s => s.VisualizarImagem(id)).ReturnsAsync(response);

            var result = await _handler.VisualizarImagem(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            _produtoControllerMock.Setup(s => s.VisualizarImagem(id)).ThrowsAsync(new Exception("Erro ao visualizar imagem"));

            var result = await _handler.VisualizarImagem(id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarOk()
        {
            int categoriaId = 1;
            var response = new ResponseBase<ProdutoResponse>();
            _produtoControllerMock.Setup(s => s.ObterProdutosPorCategoria(categoriaId)).ReturnsAsync(response);

            var result = await _handler.ObterProdutosPorCategoria(categoriaId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int categoriaId = 1;
            _produtoControllerMock.Setup(s => s.ObterProdutosPorCategoria(categoriaId)).ThrowsAsync(new Exception("Erro ao obter produtos por categoria"));

            var result = await _handler.ObterProdutosPorCategoria(categoriaId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
