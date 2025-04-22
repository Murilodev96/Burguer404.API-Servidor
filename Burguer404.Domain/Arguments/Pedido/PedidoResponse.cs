using Burguer404.Application.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;

namespace Burguer404.Application.Arguments.Pedido
{
    public class PedidoResponse : ArgumentBase
    {
        public string? CodigoPedido { get; set; }
        public string StatusPedidoDescricao { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public string DataFormatada { get; set; }
        public List<PedidoProdutoResponse> ProdutosSelecionados { get; set; }
    }
}
