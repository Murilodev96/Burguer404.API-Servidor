using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.ClassesEnums;
using Burguer404.Domain.Entities.Cliente;
using System.ComponentModel.DataAnnotations.Schema;

namespace Burguer404.Domain.Entities.Pedido
{
    public class PedidoEntity : EntityBase
    {
        public string CodigoPedido { get; set; }
        public int StatusPedidoId { get; set; }
        public virtual StatusPedidoEntity StatusPedido { get; set; }
        public int ClienteId { get; set; }
        public virtual ClienteEntity Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public ICollection<PedidoProdutoEntity> PedidoProduto { get; set; }

        [NotMapped]
        public List<int> ProdutosSelecionados { get; set; }

        [NotMapped]
        public string StatusPedidoDescricao { get; set; }
    }
}
