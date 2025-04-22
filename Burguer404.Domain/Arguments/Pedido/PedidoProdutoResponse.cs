using Burguer404.Application.Arguments.Produto;

namespace Burguer404.Domain.Arguments.Pedido
{
    public class PedidoProdutoResponse
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoResponse Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
