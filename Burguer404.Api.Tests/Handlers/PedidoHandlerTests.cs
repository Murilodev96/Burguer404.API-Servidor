using Burguer404.Api.Controllers;
using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.Entities.Pedido;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Produto;
using Microsoft.Extensions.Configuration;


namespace Burguer404.Api.Tests.Controllers
{
    public class PedidoHandlerTests
    {
        private readonly Mock<IRepositoryPedido> _pedidoRepoMock;
        private readonly Mock<IRepositoryProduto> _produtoRepoMock;
        private readonly Mock<IRepositoryMercadoPago> _mercadoPagoRepoMock;
        private readonly PedidoHandler _handler;
        private readonly Mock<IConfiguration> _config;

        public PedidoHandlerTests()
        {
            _pedidoRepoMock = new Mock<IRepositoryPedido>();
            _produtoRepoMock = new Mock<IRepositoryProduto>();
            _mercadoPagoRepoMock = new Mock<IRepositoryMercadoPago>();
            _config = new Mock<IConfiguration>();
            _handler = new PedidoHandler(_pedidoRepoMock.Object, _produtoRepoMock.Object, _config.Object);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarOk()
        {
            var request = new PedidoRequest();
            _pedidoRepoMock.Setup(r => r.CriarPedido(It.IsAny<PedidoEntity>()))
                .ReturnsAsync(new PedidoEntity());

            var result = await _handler.CadastrarPedido(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new PedidoRequest();
            _pedidoRepoMock.Setup(r => r.CriarPedido(It.IsAny<PedidoEntity>()))
                .ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _handler.CadastrarPedido(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseBase<string>>(okResult.Value);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public async Task ContinuarPagamento_DeveRetornarOk()
        {
            var request = new List<PagamentoRequest>();
            _mercadoPagoRepoMock.Setup(r => r.SolicitarQrCodeMercadoPago(It.IsAny<QrCodeRequest>()))
                .ReturnsAsync((true, "qrcode"));

            var result = await _handler.ContinuarPagamento(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ContinuarPagamento_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new List<PagamentoRequest>();
            _mercadoPagoRepoMock.Setup(r => r.SolicitarQrCodeMercadoPago(It.IsAny<QrCodeRequest>()))
                .ThrowsAsync(new Exception("Erro ao gerar QRCode"));

            var result = await _handler.ContinuarPagamento(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseBase<string>>(okResult.Value);
            Assert.False(response.Sucesso);

        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResult()
        {
            int clienteLogadoId = 1;
            _pedidoRepoMock.Setup(r => r.ListarPedidos(clienteLogadoId))
                .ReturnsAsync(new List<PedidoEntity>());

            var result = await _handler.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResultVazio_EmCasoDeErro()
        {
            int clienteLogadoId = 1;
            _pedidoRepoMock.Setup(r => r.ListarPedidos(clienteLogadoId))
                .ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _handler.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarOk()
        {
            int pedidoId = 1;
            _pedidoRepoMock.Setup(r => r.CancelarPedido(pedidoId)).ReturnsAsync(true);

            var result = await _handler.CancelarPedido(pedidoId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int pedidoId = 1;
            _pedidoRepoMock.Setup(r => r.CancelarPedido(pedidoId)).ThrowsAsync(new Exception("Erro ao cancelar"));

            var result = await _handler.CancelarPedido(pedidoId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            var pedidoEntity = new PedidoEntity
            {
                CodigoPedido = codigo,
                StatusPedidoId = 1,
                ClienteId = 1,
                DataPedido = DateTime.Now,
                PedidoProduto = new List<PedidoProdutoEntity>
        {
            new PedidoProdutoEntity
            {
                PedidoId = 1,
                ProdutoId = 1,
                Quantidade = 1,
                Produto = new ProdutoEntity
                {
                    Nome = "Hamburguer",
                    Descricao = "Delicioso",
                    Preco = 25.0,
                    CategoriaProdutoId = 1,
                    Status = true
                }
            }
        },
                StatusPedidoDescricao = "Em preparo",
                NomeCliente = "Cliente Teste",
                DataFormatada = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
            };

            _pedidoRepoMock.Setup(r => r.ObterPedidoPorCodigoPedido(codigo))
                .ReturnsAsync(pedidoEntity);

            var result = await _handler.VisualizarPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task VisualizarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _pedidoRepoMock.Setup(r => r.ObterPedidoPorCodigoPedido(codigo))
                .ThrowsAsync(new Exception("Erro ao visualizar"));

            var result = await _handler.VisualizarPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            _pedidoRepoMock.Setup(r => r.ObterPedidoPorCodigoPedido(codigo))
                .ReturnsAsync(new PedidoEntity());
            _pedidoRepoMock.Setup(r => r.AlterarStatusPedido(It.IsAny<PedidoEntity>()))
                .ReturnsAsync(true);

            var result = await _handler.AvancarStatusPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _pedidoRepoMock.Setup(r => r.ObterPedidoPorCodigoPedido(codigo))
                .ThrowsAsync(new Exception("Erro ao avançar status"));

            var result = await _handler.AvancarStatusPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
