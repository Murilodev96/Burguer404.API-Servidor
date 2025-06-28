using Burguer404.Application.Arguments.Pedido;
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
        
        [NotMapped]
        public string NomeCliente { get; set; }

        [NotMapped]
        public string DataFormatada { get; set; }


        public static PedidoEntity? MapPedido(PedidoRequest request) 
        {
            if (request.ProdutosSelecionados == null || request.ProdutosSelecionados.Count() <= 0)
                return null;

            return new PedidoEntity() { 
                ClienteId = request.ClienteId, 
                ProdutosSelecionados = request.ProdutosSelecionados, 
                DataPedido = request.DataPedido!.Value, 
                StatusPedidoId = request.StatusPedidoId 
            };
        }

    }
}
