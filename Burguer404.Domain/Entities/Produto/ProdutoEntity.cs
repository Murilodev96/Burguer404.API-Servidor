using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.ClassesEnums;
using Burguer404.Domain.Entities.Pedido;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Burguer404.Domain.Entities.Produto
{
    public class ProdutoEntity : EntityBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int CategoriaProdutoId { get; set; }
        public byte[]? ImagemByte { get; set; }
        public virtual CategoriaPedidoEntity CategoriaProduto { get; set; }
        public ICollection<PedidoProdutoEntity> PedidoProduto { get; set; } = [];
        public bool Status { get; set; } = true;
        [NotMapped]
        public string ImagemBase64 { get; set; }
    }
}
