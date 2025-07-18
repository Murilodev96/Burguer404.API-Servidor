using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;

using Burguer404.Application.Ports.Gateways;

namespace Burguer404.Application.Gateways
{
    public class PedidosGateway : IPedidosGateway
    {
        private readonly IRepositoryPedido _repository;

        public PedidosGateway(IRepositoryPedido repository)
        {
            _repository = repository;
        }

        public async Task<PedidoEntity?> AtualizarStatusPagamentoAsync(PedidoEntity pedido)
            => await _repository.AtualizarPedido(pedido);
        
        public async Task<PedidoEntity?> CriarPedidoAsync(PedidoEntity pedido)
            => await _repository.CriarPedido(pedido);

        public async Task InserirProdutosNoPedidoAsync(List<PedidoProdutoEntity> pedidoProdutos)
            => await _repository.InserirProdutosNoPedido(pedidoProdutos);

        public async Task<PedidoEntity?> ObterPedidoPorCodigoPedidoAsync(string codigo)
            => await _repository.ObterPedidoPorCodigoPedido(codigo);

        public async Task<bool> AlterarStatusPedidoAsync(PedidoEntity pedido)
            => await _repository.AlterarStatusPedido(pedido);

        public async Task<List<PedidoEntity>?> ListarPedidosAsync(int clienteId)
            => await _repository.ListarPedidos(clienteId);

        public async Task<bool> CancelarPedidoAsync(int pedidoId)
            => await _repository.CancelarPedido(pedidoId);
    }
}
