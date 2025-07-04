using Burguer404.Application.Gateways;
using Burguer404.Domain.Enums;

namespace Burguer404.Application.UseCases.Webhook
{
    public class AtualizarPagamentoPedidoUseCase
    {
        private readonly PedidosGateway _pedidosGateway;

        public AtualizarPagamentoPedidoUseCase(PedidosGateway pedidosGateway)
        {
            _pedidosGateway = pedidosGateway;
        }

        public static AtualizarPagamentoPedidoUseCase Create(PedidosGateway pedidosGateway)
        {
            return new AtualizarPagamentoPedidoUseCase(pedidosGateway);
        }

        public async Task<bool> ExecuteAsync(string codigoPedido, string status) 
        {
            var pedido = await _pedidosGateway.ObterPedidoPorCodigoPedidoAsync(codigoPedido);
            if (pedido == null)
                return false;

            int novoStatus = status.ToLower() switch
            {
                "approved" => (int)EnumStatusPedido.EmPreparacao,
                "rejected" or "cancelled" => (int)EnumStatusPedido.Cancelado,
                "pending" or "in_process" => (int)EnumStatusPedido.AguardandoPagamento,
                _ => pedido.StatusPedidoId
            };

            pedido.StatusPedidoId = novoStatus;
            pedido = await _pedidosGateway.AtualizarStatusPagamentoAsync(pedido);

            return true;
        }

    }
}
