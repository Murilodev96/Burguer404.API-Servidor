using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Moq;

namespace Burguer404.Api.Tests.UseCases
{
    public class PedidoUseCasesTests
    {
        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarSucesso()
        {
            var codigo = "ABC123";
            var pedido = new PedidoEntity { StatusPedidoId = 1 };
            var repositoryMock = new Mock<IRepositoryPedido>();
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(codigo)).ReturnsAsync(pedido);
            gatewayMock.Setup(g => g.AlterarStatusPedidoAsync(It.IsAny<PedidoEntity>())).ReturnsAsync(true);
            var useCase = new AvancarStatusPedidoUseCase(gatewayMock.Object);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(codigo);
            Assert.True(sucesso);
            Assert.Equal("Status do pedido alterado com sucesso", mensagem);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarFalha_QuandoPedidoNaoEncontrado()
        {
            var codigo = "ABC123";
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(codigo)).ReturnsAsync((PedidoEntity)null!);
            var useCase = new AvancarStatusPedidoUseCase(gatewayMock.Object);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(codigo);
            Assert.False(sucesso);
            Assert.Equal("Pedido não encontrado!", mensagem);
        }

        [Fact]
        public async Task AvancarStatusPedido_DeveRetornarFalha_QuandoErroAoAlterarStatus()
        {
            var codigo = "ABC123";
            var pedido = new PedidoEntity { StatusPedidoId = 1 };
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(codigo)).ReturnsAsync(pedido);
            gatewayMock.Setup(g => g.AlterarStatusPedidoAsync(It.IsAny<PedidoEntity>())).ReturnsAsync(false);
            var useCase = new AvancarStatusPedidoUseCase(gatewayMock.Object);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(codigo);
            Assert.False(sucesso);
            Assert.Equal("Ocorreu um erro durante a tentativa de alteração de status do pedido!", mensagem);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarMensagemSucesso()
        {
            var request = new PedidoRequest { ProdutosSelecionados = new List<int> { 1, 2 } };
            var pedido = new PedidoEntity { Id = 1, ProdutosSelecionados = request.ProdutosSelecionados };
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.CriarPedidoAsync(It.IsAny<PedidoEntity>())).ReturnsAsync(pedido);
            gatewayMock.Setup(g => g.InserirProdutosNoPedidoAsync(It.IsAny<List<PedidoProdutoEntity>>())).Returns(Task.CompletedTask);
            var useCase = new CadastrarPedidoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);
            Assert.Equal("Pedido realizado com sucesso!", result);
        }

        [Fact]
        public async Task CadastrarPedido_DeveRetornarStringVazia_QuandoMapeamentoFalha()
        {
            var request = new PedidoRequest { ProdutosSelecionados = null! };
            var gatewayMock = new Mock<IPedidosGateway>();
            var useCase = new CadastrarPedidoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(request);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarTrue()
        {
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.CancelarPedidoAsync(It.IsAny<int>())).ReturnsAsync(true);
            var useCase = new CancelarPedidoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task CancelarPedido_DeveRetornarFalse_QuandoFalha()
        {
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.CancelarPedidoAsync(It.IsAny<int>())).ReturnsAsync(false);
            var useCase = new CancelarPedidoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task ListarPedidos_DeveRetornarLista()
        {
            var pedidos = new List<PedidoEntity> { new PedidoEntity { Id = 1 } };
            var gatewayMock = new Mock<IPedidosGateway>();
            gatewayMock.Setup(g => g.ListarPedidosAsync(It.IsAny<int>())).ReturnsAsync(pedidos);
            var useCase = new ListarPedidoUseCase(gatewayMock.Object);
            var result = await useCase.ExecuteAsync(1);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarPedidoEProdutos()
        {
            var codigo = "ABC123";
            var pedido = new PedidoEntity { Id = 1, PedidoProduto = new List<PedidoProdutoEntity> { new PedidoProdutoEntity { ProdutoId = 1, PedidoId = 1, Quantidade = 1 } } };
            var produto = new ProdutoEntity { Id = 1, Nome = "Hamburguer" };
            var pedidoGatewayMock = new Mock<IPedidosGateway>();
            var produtoGatewayMock = new Mock<IProdutoGateway>();
            pedidoGatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(codigo)).ReturnsAsync(pedido);
            produtoGatewayMock.Setup(g => g.ObterProdutoPorIdAsync(1)).ReturnsAsync(produto);
            var useCase = new VisualizarPedidoUseCase(pedidoGatewayMock.Object, produtoGatewayMock.Object);
            var (pedidoResult, produtosResult) = await useCase.ExecuteAsync(codigo);
            Assert.NotNull(pedidoResult);
            Assert.NotNull(produtosResult);
            Assert.Single(produtosResult);
            Assert.Equal("Hamburguer", produtosResult[0].Produto.Nome);
        }

        [Fact]
        public async Task VisualizarPedido_DeveRetornarNull_QuandoCodigoInvalido()
        {
            var pedidoGatewayMock = new Mock<IPedidosGateway>();
            var produtoGatewayMock = new Mock<ProdutoGateway>(null!);
            var useCase = new VisualizarPedidoUseCase(pedidoGatewayMock.Object, produtoGatewayMock.Object);
            var (pedidoResult, produtosResult) = await useCase.ExecuteAsync("");
            Assert.Null(pedidoResult);
            Assert.Null(produtosResult);
        }
    }
}