using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Produto;
using Burguer404.Domain.Ports.Services.Produto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Burguer.Api.Tests.Controllers
{
    public class ProdutoControllerTests
    {
        private readonly Mock<IServiceProduto> _serviceMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTests()
        {
            _serviceMock = new Mock<IServiceProduto>();
            _controller = new ProdutoController(_serviceMock.Object);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest();
            var response = new ResponseBase<ProdutoResponse>();
            _serviceMock.Setup(s => s.CadastrarProduto(request)).ReturnsAsync(response);

            var result = await _controller.CadastrarProduto(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
            _serviceMock.Setup(s => s.CadastrarProduto(request)).ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _controller.CadastrarProduto(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarJsonResult()
        {
            var response = new ResponseBase<ProdutoResponse>();
            _serviceMock.Setup(s => s.ListarProdutos()).ReturnsAsync(response);

            var result = await _controller.ListarProdutos();

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarProdutos_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _serviceMock.Setup(s => s.ListarProdutos()).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _controller.ListarProdutos();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarOk()
        {
            var request = new ProdutoRequest();
            var response = new ResponseBase<ProdutoResponse>();
            _serviceMock.Setup(s => s.AtualizarProduto(request)).ReturnsAsync(response);

            var result = await _controller.AtualizarProduto(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AtualizarProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new ProdutoRequest();
            _serviceMock.Setup(s => s.AtualizarProduto(request)).ThrowsAsync(new Exception("Erro ao atualizar"));

            var result = await _controller.AtualizarProduto(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarOk()
        {
            int id = 1;
            var response = new ResponseBase<bool>();
            _serviceMock.Setup(s => s.RemoverProduto(id)).ReturnsAsync(response);

            var result = await _controller.RemoverProduto(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            _serviceMock.Setup(s => s.RemoverProduto(id)).ThrowsAsync(new Exception("Erro ao remover"));

            var result = await _controller.RemoverProduto(id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarOk()
        {
            var response = new ResponseBase<CardapioResponse>();
            _serviceMock.Setup(s => s.ObterCardapio()).ReturnsAsync(response);

            var result = await _controller.ObterCardapio();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterCardapio_DeveRetornarBadRequest_EmCasoDeErro()
        {
            _serviceMock.Setup(s => s.ObterCardapio()).ThrowsAsync(new Exception("Erro ao obter cardápio"));

            var result = await _controller.ObterCardapio();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarOk()
        {
            int id = 1;
            var response = new ResponseBase<string>();
            _serviceMock.Setup(s => s.VisualizarImagem(id)).ReturnsAsync(response);

            var result = await _controller.VisualizarImagem(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarImagem_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int id = 1;
            _serviceMock.Setup(s => s.VisualizarImagem(id)).ThrowsAsync(new Exception("Erro ao visualizar imagem"));

            var result = await _controller.VisualizarImagem(id);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarOk()
        {
            int categoriaId = 1;
            var response = new ResponseBase<ProdutoResponse>();
            _serviceMock.Setup(s => s.ObterProdutosPorCategoria(categoriaId)).ReturnsAsync(response);

            var result = await _controller.ObterProdutosPorCategoria(categoriaId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObterProdutosPorCategoria_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int categoriaId = 1;
            _serviceMock.Setup(s => s.ObterProdutosPorCategoria(categoriaId)).ThrowsAsync(new Exception("Erro ao obter produtos por categoria"));

            var result = await _controller.ObterProdutosPorCategoria(categoriaId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
