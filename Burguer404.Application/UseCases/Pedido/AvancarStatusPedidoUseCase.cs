using Burguer404.Domain.Ports.Repositories.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public sealed class AvancarStatusPedidoUseCase
    {
        private readonly IRepositoryPedido _pedidoRepository;

        public AvancarStatusPedidoUseCase(IRepositoryPedido pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> ExecuteAsync(string codigo)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorCodigoPedido(codigo);
            return await _pedidoRepository.AlterarStatusPedido(pedido);
        }
    }
}