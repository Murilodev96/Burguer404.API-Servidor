using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Domain.Entities.Pedido
{
    public class PedidoProdutoEntity : EntityBase
    {
        public int PedidoId { get; set; }
        public virtual PedidoEntity Pedido { get; set; }
        public int ProdutoId { get; set; }
        public virtual ProdutoEntity Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
