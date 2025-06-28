using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Pedido
{
    public class VisualizarPedidoUseCase
    {
        private readonly PedidosGateway _pedidoGateway;
        private readonly ProdutoGateway _produtoGateway;

        public VisualizarPedidoUseCase(PedidosGateway pedidoGateway, ProdutoGateway produtoGateway)
        {
            _pedidoGateway = pedidoGateway;
            _produtoGateway = produtoGateway;
        }

        public static VisualizarPedidoUseCase Create(PedidosGateway pedidoGateway, ProdutoGateway produtoGateway)
        {
            return new VisualizarPedidoUseCase(pedidoGateway, produtoGateway);
        }

        public async Task<(PedidoEntity?, List<PedidoProdutoEntity>?)> ExecuteAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return (null, null);

            var pedido =  await _pedidoGateway.ObterPedidoPorCodigoPedidoAsync(codigo);

            if(!(pedido is PedidoEntity))
                return (null, null);

            var listaPedidoProdutos = new List<PedidoProdutoEntity>();

            foreach (var item in pedido.PedidoProduto)
            {
                var produto = await _produtoGateway.ObterProdutoPorIdAsync(item.ProdutoId);

                var pedProd = new PedidoProdutoEntity()
                {
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Produto = new ProdutoEntity() 
                    {
                        CategoriaProdutoId = produto.CategoriaProdutoId,
                        CategoriaProduto = produto.CategoriaProduto,
                        Descricao = produto.Descricao,
                        Id = item.Id,
                        ImagemBase64 = produto.ImagemBase64,
                        ImagemByte = produto.ImagemByte,
                        Nome = produto.Nome,
                        Preco = produto.Preco,
                        Status = produto.Status,
                    }
                };

                listaPedidoProdutos.Add(pedProd);
            }

            return (pedido, listaPedidoProdutos);
        }
    }
}