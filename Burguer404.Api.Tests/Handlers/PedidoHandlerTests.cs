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
using Burguer404.Application.Arguments.Produto;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Application.Controllers;
using Burguer404.Application.Ports.Gateways;


namespace Burguer404.Api.Tests.Handlers
{
    public class PedidoHandlerTests
    {

        private readonly PedidoHandler _handler;
        private readonly PedidosController _pedidoController;
        private readonly Mock<IConfiguration> _config;

        private readonly Mock<IPedidosGateway> _pedidosGatewayMock;
        private readonly Mock<IProdutoGateway> _produtoGatewayMock;
        private readonly CadastrarPedidoUseCase _cadastrarPedidoUseCase;
        private readonly ListarPedidoUseCase _listarPedidoUseCase;
        private readonly VisualizarPedidoUseCase _visualizarPedidoUseCase;
        private readonly AvancarStatusPedidoUseCase _avancarStatusPedidoUseCase;
        private readonly CancelarPedidoUseCase _cancelarPedidoUseCase;
        private readonly GerarQrCodeUseCase _gerarQrCodeUseCase;
        private readonly Mock<IRepositoryPedido> _pedidoRepoMock;

        public PedidoHandlerTests()
        {

            _config = new Mock<IConfiguration>();
            _pedidoRepoMock = new Mock<IRepositoryPedido>();
            _pedidosGatewayMock = new Mock<IPedidosGateway>();
            _produtoGatewayMock = new Mock<IProdutoGateway>();
            _cadastrarPedidoUseCase = new CadastrarPedidoUseCase(_pedidosGatewayMock.Object);
            _listarPedidoUseCase = new ListarPedidoUseCase(_pedidosGatewayMock.Object);
            _visualizarPedidoUseCase = new VisualizarPedidoUseCase(_pedidosGatewayMock.Object, _produtoGatewayMock.Object);
            _avancarStatusPedidoUseCase = new AvancarStatusPedidoUseCase(_pedidosGatewayMock.Object);
            _cancelarPedidoUseCase = new CancelarPedidoUseCase(_pedidosGatewayMock.Object);
            _gerarQrCodeUseCase = new GerarQrCodeUseCase(_pedidosGatewayMock.Object, _produtoGatewayMock.Object);

            _pedidoController = new PedidosController(
                _cadastrarPedidoUseCase,
                _listarPedidoUseCase,
                _visualizarPedidoUseCase,
                _avancarStatusPedidoUseCase,
                _cancelarPedidoUseCase,
                _gerarQrCodeUseCase,
                _config.Object);

            _handler = new PedidoHandler(_pedidoController);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarOk()
        {
            var request = new PedidoRequest();
            _pedidosGatewayMock.Setup(x => x.CriarPedidoAsync(It.IsAny<PedidoEntity>()))
                .ReturnsAsync(new PedidoEntity { CodigoPedido = "some_order_code" });

            var result = await _handler.CadastrarPedido(request);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var request = new PedidoRequest
            {
                ClienteId = 1,
                ProdutosSelecionados = new List<int> { 1 },
                DataPedido = DateTime.Now,
                StatusPedidoId = 1
            };
            _pedidosGatewayMock.Setup(x => x.CriarPedidoAsync(It.IsAny<PedidoEntity>()))
                .ThrowsAsync(new Exception("Erro ao cadastrar"));

            var result = await _handler.CadastrarPedido(request);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ResponseBase<string>>(badRequestResult.Value);
            Assert.Equal("Erro ao cadastrar", response.Mensagem);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResult()
        {
            int clienteLogadoId = 1;
            _pedidosGatewayMock.Setup(x => x.ListarPedidosAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<PedidoEntity>());

            var result = await _handler.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarJsonResultVazio_EmCasoDeErro()
        {
            int clienteLogadoId = 1;
            _pedidosGatewayMock.Setup(x => x.ListarPedidosAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Erro ao listar"));

            var result = await _handler.ListarPedidos(clienteLogadoId);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarOk()
        {
            int pedidoId = 1;
            _pedidosGatewayMock.Setup(x => x.CancelarPedidoAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _handler.CancelarPedido(pedidoId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            int pedidoId = 1;
            _pedidosGatewayMock.Setup(x => x.CancelarPedidoAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Erro ao cancelar"));

            var result = await _handler.CancelarPedido(pedidoId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            var cliente = new Burguer404.Domain.Entities.Cliente.ClienteEntity { Id = 1, Nome = "Cliente Teste" };
            var statusPedido = new Burguer404.Domain.Entities.ClassesEnums.StatusPedidoEntity { Id = 1, Descricao = "Em andamento" };
            var pedidoProduto = new PedidoProdutoEntity { ProdutoId = 1, PedidoId = 1, Quantidade = 1 };
            var pedido = new PedidoEntity {
                CodigoPedido = codigo,
                PedidoProduto = new List<PedidoProdutoEntity> { pedidoProduto },
                Cliente = cliente,
                StatusPedido = statusPedido,
                ClienteId = 1,
                StatusPedidoId = 1,
                DataPedido = DateTime.Now
            };
            _pedidosGatewayMock.Setup(x => x.ObterPedidoPorCodigoPedidoAsync(It.IsAny<string>()))
                .ReturnsAsync(pedido);
            _produtoGatewayMock.Setup(x => x.ObterProdutoPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProdutoEntity { Id = 1, Nome = "Produto Teste", Preco = 10, CategoriaProdutoId = 1, CategoriaProduto = null, Descricao = "desc", ImagemBase64 = "", ImagemByte = null, Status = true });
       
            var result = await _handler.VisualizarPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _pedidosGatewayMock.Setup(x => x.ObterPedidoPorCodigoPedidoAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Erro ao visualizar"));

            var result = await _handler.VisualizarPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarOk()
        {
            var codigo = "ABC123";
            _pedidosGatewayMock.Setup(x => x.ObterPedidoPorCodigoPedidoAsync(It.IsAny<string>()))
                .ReturnsAsync(new PedidoEntity { CodigoPedido = codigo, StatusPedidoId = 1 });
            _pedidosGatewayMock.Setup(x => x.AlterarStatusPedidoAsync(It.IsAny<PedidoEntity>()))
                .ReturnsAsync(true);

            var result = await _handler.AvancarStatusPedido(codigo);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarBadRequest_EmCasoDeErro()
        {
            var codigo = "ABC123";
            _pedidosGatewayMock.Setup(x => x.ObterPedidoPorCodigoPedidoAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Erro ao avançar status"));

            var result = await _handler.AvancarStatusPedido(codigo);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
