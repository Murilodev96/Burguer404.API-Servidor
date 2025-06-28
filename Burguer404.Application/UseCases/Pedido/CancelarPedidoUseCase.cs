using Burguer404.Application.Gateways;

namespace Burguer404.Application.UseCases.Pedido
{
    public sealed class CancelarPedidoUseCase
    {
        private readonly PedidosGateway _pedidoGateway;

        public CancelarPedidoUseCase(PedidosGateway pedidoGateway)
        {
            _pedidoGateway = pedidoGateway;
        }

        public static CancelarPedidoUseCase Create(PedidosGateway pedidoGateway)
        {
            return new CancelarPedidoUseCase(pedidoGateway);
        }

        public async Task<bool> ExecuteAsync(int pedidoId) =>
            await _pedidoGateway.CancelarPedidoAsync(pedidoId);

    }
}