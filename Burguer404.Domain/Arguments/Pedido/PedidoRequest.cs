using Burguer404.Application.Arguments.Base;
using Burguer404.Domain.Enums;

namespace Burguer404.Application.Arguments.Pedido
{
    public class PedidoRequest : ArgumentBase
    {
        public string? CodigoPedido { get; set; }
        public EnumStatusPedido? StatusPedido { get; set; } = EnumStatusPedido.Recebido;
        public int ClienteId { get; set; }
        public DateTime? DataPedido { get; set; } = DateTime.Now;
        public List<int> ProdutosSelecionados { get; set; }
    }
}
