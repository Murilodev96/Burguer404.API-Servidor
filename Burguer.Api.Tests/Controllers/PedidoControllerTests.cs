using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Services.Pedido;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Burguer.Api.Tests.Controllers
{
    public class PedidoControllerTests
    {
        private readonly Mock<IServicePedido> _serviceMock;
        private readonly PedidoController _controller;

        public PedidoControllerTests()
        {
            _serviceMock = new Mock<IServicePedido>();
            _controller = new PedidoController(_serviceMock.Object);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarOk()
        {
            var request = new PedidoRequest();
            var response = new ResponseBase<string>();
            _serviceMock.Setup(s => s.CadastrarPedido(request)).ReturnsAsync(response);

            var result = await _controller.CadastrarPedido(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new PedidoRequest();
            _serviceMock.Setup(s => s.CadastrarPedido(request)).ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _controller.CadastrarPedido(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ContinuarPagamento_DeveRetornarOk()
        {
            var request = new List<PagamentoRequest>();
            var response = new ResponseBase<string>();
            _serviceMock.Setup(s => s.GerarQrCode(request)).ReturnsAsync(response);

            var result = await _controller.ContinuarPagamento(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ContinuarPagamento_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new List<PagamentoRequest>();
            _serviceMock.Setup(s => s.GerarQrCode(request)).ThrowsAsync(new Exception("Erro ao gerar QRCode"));

            var result = await _controller.ContinuarPagamento(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResult()
        {
            int clienteLogadoId = 1;
            var response = new ResponseBase<PedidoResponse>();
            _serviceMock.Setup(s => s.ListarPedidos(clienteLogadoId)).ReturnsAsync(response);

            var result = await _controller.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResultVazio_EmCasoDeErro()
        {
            int clienteLogadoId = 1;
            _serviceMock.Setup(s => s.ListarPedidos(clienteLogadoId)).ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _controller.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarOk()
        {
            int pedidoId = 1;
            var response = new ResponseBase<bool>();
            _serviceMock.Setup(s => s.CancelarPedido(pedidoId)).ReturnsAsync(response);

            var result = await _controller.CancelarPedido(pedidoId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int pedidoId = 1;
            _serviceMock.Setup(s => s.CancelarPedido(pedidoId)).ThrowsAsync(new Exception("Erro ao cancelar"));

            var result = await _controller.CancelarPedido(pedidoId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            var response = new ResponseBase<PedidoResponse>();
            _serviceMock.Setup(s => s.VisualizarPedido(codigo)).ReturnsAsync(response);

            var result = await _controller.VisualizarPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _serviceMock.Setup(s => s.VisualizarPedido(codigo)).ThrowsAsync(new Exception("Erro ao visualizar"));

            var result = await _controller.VisualizarPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            var response = new ResponseBase<bool>();
            _serviceMock.Setup(s => s.AvancarStatusPedido(codigo)).ReturnsAsync(response);

            var result = await _controller.AvancarStatusPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _serviceMock.Setup(s => s.AvancarStatusPedido(codigo)).ThrowsAsync(new Exception("Erro ao avançar status"));

            var result = await _controller.AvancarStatusPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
