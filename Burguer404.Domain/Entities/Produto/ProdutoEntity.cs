using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.ClassesEnums;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Domain.Entities.Produto
{
    public class ProdutoEntity : EntityBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public byte[] Imagem { get; set; }
        public int CategoriaPedidoId { get; set; }
        public virtual CategoriaPedidoEntity CategoriaPedido { get; set; }
        public ICollection<PedidoProdutoEntity> PedidoProduto { get; set; } = [];
        public bool Status { get; set; }
    }
}
