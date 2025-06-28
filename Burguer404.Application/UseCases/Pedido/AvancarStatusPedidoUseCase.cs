using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public  class AvancarStatusPedidoUseCase
    {
        private readonly PedidosGateway _pedidoGateway;

        public AvancarStatusPedidoUseCase(PedidosGateway pedidoGateway)
        {
            _pedidoGateway = pedidoGateway;
        }

        public static AvancarStatusPedidoUseCase Create(PedidosGateway produtoGateway)
        {
            return new AvancarStatusPedidoUseCase(produtoGateway);
        }

        public async Task<(bool, string)> ExecuteAsync(string codigo)
        {
            var pedido = await _pedidoGateway.ObterPedidoPorCodigoPedidoAsync(codigo);

            if (!(pedido is PedidoEntity))
                return (false, "Pedido não encontrado!");

            pedido!.StatusPedidoId = ValidarPedido.ValidacoesDeStatusDePedido(pedido.StatusPedidoId);

            var pedidoAlterado = await _pedidoGateway.AlterarStatusPedidoAsync(pedido);

            if (!pedidoAlterado)
                return (pedidoAlterado, "Ocorreu um erro durante a tentativa de alteração de status do pedido!");

            return (pedidoAlterado, "Status do pedido alterado com sucesso");
        }
    }
}