using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;

namespace Burguer404.Application.UseCases.Pedido
{
    public class CancelarPedidoUseCase
    {
        private readonly IPedidosGateway _pedidoGateway;

        public CancelarPedidoUseCase(IPedidosGateway pedidoGateway)
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