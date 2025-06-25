using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;

namespace Burguer404.Domain.UseCases.Pedido
{
    public sealed class ListarPedidosUseCase
    {
        private readonly IRepositoryPedido _pedidoRepository;

        public ListarPedidosUseCase(IRepositoryPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoEntity>> ExecuteAsync(int clienteId)
        {
            var pedidos = await _pedidoRepository.ListarPedidos(clienteId);
            
            return pedidos;
        }
    }
}