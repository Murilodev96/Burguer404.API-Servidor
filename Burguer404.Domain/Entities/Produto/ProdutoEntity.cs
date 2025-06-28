using Burguer404.Application.Arguments.Produto;
using Burguer404.Domain.Entities.Base;
using Burguer404.Domain.Entities.ClassesEnums;
using Burguer404.Domain.Entities.Pedido;
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

        public static ProdutoEntity? MapProduto(ProdutoRequest request) 
        {
            if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.Nome) || request.Preco <= 0 || request.CategoriaProdutoId <= 0)
                return null;

            return new ProdutoEntity() { 
                Nome = request.Nome, 
                Descricao = request.Descricao,
                Preco = request.Preco,
                CategoriaProdutoId = request.CategoriaProdutoId,
                ImagemByte = request.ImagemByte,
                Status = request.Status ?? false,
                ImagemBase64 = string.Empty,
            };
        }
    }
}
