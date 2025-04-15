using Burguer404.Application.Arguments.Base;

namespace Burguer404.Application.Arguments.Pedido
{
    public class PedidoResponse : ArgumentBase
    {
        public string? CodigoPedido { get; set; }
        public string StatusPedidoDescricao { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public List<int> ProdutosSelecionados { get; set; }
    }
}
