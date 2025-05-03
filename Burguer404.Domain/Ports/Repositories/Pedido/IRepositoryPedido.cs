using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Domain.Ports.Repositories.Pedido
{
    public interface IRepositoryPedido
    {
        Task<PedidoEntity> CriarPedido(PedidoEntity pedido);
        Task InserirProdutosNoPedido(List<PedidoProdutoEntity> pedidoProdutos);
        Task<List<PedidoEntity>> ListarPedidos();
        Task<bool> CancelarPedido(int pedidoId);
        Task<PedidoEntity?> ObterPedidoPorCodigoPedido(string codigo);
        Task<bool> AlterarStatusPedido(PedidoEntity pedido);
    }
}
