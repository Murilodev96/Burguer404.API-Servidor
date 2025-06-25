using Burguer404.Domain.Ports.Repositories.Pedido;

namespace Burguer404.Domain.UseCases.Pedido
{
    public sealed class CancelarPedidoUseCase
    {
        private readonly IRepositoryPedido _pedidoRepository;

        public CancelarPedidoUseCase(IRepositoryPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> ExecuteAsync(int pedidoId) =>
            await _pedidoRepository.CancelarPedido(pedidoId);

    }
}