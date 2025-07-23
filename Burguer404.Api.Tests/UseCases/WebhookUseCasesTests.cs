using Burguer404.Application.UseCases.Webhook;
using Burguer404.Domain.Arguments.Webhook;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Interfaces.Gateways;
using Moq;

namespace Burguer404.Api.Tests.UseCases
{
    public class WebhookUseCasesTests
    {
        private readonly IPedidosGateway _pedidosGateway;

        public WebhookUseCasesTests()
        {
            _pedidosGateway = new Mock<IPedidosGateway>().Object;
        }
        [Theory]
        [InlineData("approved", 1)]
        [InlineData("rejected", 5)]
        [InlineData("cancelled", 5)]

        public async Task AtualizarPagamentoPedido_DeveAtualizarStatusCorretamente(string status, int esperado)
        {
            var codigoPedido = "ABC123";
            var pedido = new PedidoEntity { StatusPedidoId = 6 };
            var gatewayMock = Mock.Get(_pedidosGateway);
            gatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(codigoPedido)).ReturnsAsync(pedido);
            gatewayMock.Setup(g => g.AtualizarStatusPagamentoAsync(It.IsAny<PedidoEntity>())).ReturnsAsync(pedido);
            var useCase = new AtualizarPagamentoPedidoUseCase(_pedidosGateway);
            var result = await useCase.ExecuteAsync(codigoPedido, status);
            Assert.True(result);
            Assert.Equal(esperado, pedido.StatusPedidoId);
        }

        [Fact]
        public async Task AtualizarPagamentoPedido_DeveRetornarFalse_QuandoPedidoNaoEncontrado()
        {
            var gatewayMock = Mock.Get(_pedidosGateway);
            gatewayMock.Setup(g => g.ObterPedidoPorCodigoPedidoAsync(It.IsAny<string>())).ReturnsAsync((PedidoEntity)null!);
            var useCase = new AtualizarPagamentoPedidoUseCase(_pedidosGateway);
            var result = await useCase.ExecuteAsync("ABC123", "approved");
            Assert.False(result);
        }

        [Fact]
        public async Task ValidarNotificacao_DeveLancarExcecao_QuandoPayloadInvalido()
        {
            var useCase = new ValidarNotificacaoUseCase();
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(null!));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(new NotificacaoWebhook { type = null! }));
        }

        [Fact]
        public async Task ValidarNotificacao_DeveLancarExcecao_QuandoEventoNaoPagamento()
        {
            var useCase = new ValidarNotificacaoUseCase();
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(new NotificacaoWebhook { type = "outro" }));
        }

        [Fact]
        public async Task ValidarNotificacao_DeveExecutar_QuandoEventoPagamento()
        {
            var useCase = new ValidarNotificacaoUseCase();
            var notificacao = new NotificacaoWebhook { type = "payment" };
            await useCase.ExecuteAsync(notificacao);
        }
    }
}