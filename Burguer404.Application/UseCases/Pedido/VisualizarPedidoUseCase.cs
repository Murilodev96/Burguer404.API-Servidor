using Burguer404.Application.Gateways;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Entities.Produto;

namespace Burguer404.Application.UseCases.Pedido
{
    public class VisualizarPedidoUseCase
    {
        private readonly IPedidosGateway _pedidosGateway;
        private readonly IProdutoGateway _produtoGateway;

        public VisualizarPedidoUseCase(IPedidosGateway pedidosGateway, IProdutoGateway produtoGateway)
        {
            _pedidosGateway = pedidosGateway;
            _produtoGateway = produtoGateway;
        }

        public static VisualizarPedidoUseCase Create(PedidosGateway pedidoGateway, IProdutoGateway produtoGateway)
        {
            return new VisualizarPedidoUseCase(pedidoGateway, produtoGateway);
        }

        public async Task<(PedidoEntity?, List<PedidoProdutoEntity>?)> ExecuteAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return (null, null);

            var pedido =  await _pedidosGateway.ObterPedidoPorCodigoPedidoAsync(codigo);

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