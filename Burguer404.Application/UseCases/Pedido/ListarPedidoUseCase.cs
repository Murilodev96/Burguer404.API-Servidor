using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public class ListarPedidoUseCase
    {
        private readonly IPedidosGateway _pedidosGateway;

        public ListarPedidoUseCase(IPedidosGateway pedidosGateway)
        {
            _pedidosGateway = pedidosGateway;
        }

        public async Task<List<PedidoEntity>?> ExecuteAsync(int clienteId)
            => await _pedidosGateway.ListarPedidosAsync(clienteId);

    }
}