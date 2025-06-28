using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public class CadastrarPedidoUseCase
    {
        private readonly PedidosGateway _pedidoGateway;

        public CadastrarPedidoUseCase(PedidosGateway pedidoGateway)
        {
            _pedidoGateway = pedidoGateway;
        }

        public static CadastrarPedidoUseCase Create(PedidosGateway produtoGateway)
        {
            return new CadastrarPedidoUseCase(produtoGateway);
        }

        public async Task<string> ExecuteAsync(PedidoRequest request)
        {
            var pedido = PedidoEntity.MapPedido(request);

            if (!(pedido is PedidoEntity))
                return string.Empty;

            pedido = await _pedidoGateway.CriarPedidoAsync(pedido);

            if (pedido?.Id <= 0)
                return string.Empty;

            pedido!.PedidoProduto = [.. pedido.ProdutosSelecionados
                                            .GroupBy(id => id)
                                            .Select(g => new PedidoProdutoEntity
                                            {
                                                PedidoId  = pedido.Id,
                                                ProdutoId = g.Key,
                                                Quantidade = g.Count()
                                            })];

            await _pedidoGateway.InserirProdutosNoPedidoAsync([.. pedido.PedidoProduto]);
           
            return "Pedido realizado com sucesso!";
        }
    }
}