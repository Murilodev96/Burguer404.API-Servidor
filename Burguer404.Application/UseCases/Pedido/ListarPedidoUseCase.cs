using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public class ListarPedidosUseCase
    {
        private readonly PedidosGateway _pedidoGateway;

        public ListarPedidosUseCase(PedidosGateway pedidoGateway)
        {
            _pedidoGateway = pedidoGateway;
        }

        public static ListarPedidosUseCase Create(PedidosGateway produtoGateway)
        {
            return new ListarPedidosUseCase(produtoGateway);
        }

        public async Task<List<PedidoEntity>?> ExecuteAsync(int clienteId)
            => await _pedidoGateway.ListarPedidosAsync(clienteId);

    }
}