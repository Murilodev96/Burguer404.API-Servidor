using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;
using Burguer404.Domain.Arguments.Pedido;

namespace Burguer404.Application.Ports.Gateways
{
    public interface IPedidosGateway
    {
        Task<PedidoEntity?> AtualizarStatusPagamentoAsync(PedidoEntity pedido);
        Task<PedidoEntity?> CriarPedidoAsync(PedidoEntity pedido);
        Task InserirProdutosNoPedidoAsync(List<PedidoProdutoEntity> pedidoProdutos);
        Task<PedidoEntity?> ObterPedidoPorCodigoPedidoAsync(string codigo);
        Task<bool> AlterarStatusPedidoAsync(PedidoEntity pedido);
        Task<List<PedidoEntity>?> ListarPedidosAsync(int clienteId);
        Task<bool> CancelarPedidoAsync(int pedidoId);
    }
}