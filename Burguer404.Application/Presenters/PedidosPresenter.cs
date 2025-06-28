using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;

namespace Burguer404.Application.Presenters
{
    public static class PedidosPresenter
    {
        public static ResponseBase<PedidoResponse> ObterListaPedidoResponse(List<PedidoEntity> pedidos) 
        {
            var listaPedidoMap = new List<PedidoResponse>();

            foreach (var pedido in pedidos)
            {
                var pedidoMap = new PedidoResponse() 
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    CodigoPedido = pedido.CodigoPedido,
                    DataFormatada = pedido.DataFormatada,
                    DataPedido = pedido.DataPedido,
                    NomeCliente = pedido.Cliente.Nome,
                    StatusPedidoDescricao = pedido.StatusPedido.Descricao
                };

                listaPedidoMap.Add(pedidoMap);
            }

            return new ResponseBase<PedidoResponse>() { Sucesso = true, Mensagem = "Pedidos listados com sucesso", Resultado = listaPedidoMap };
        }

        public static ResponseBase<PedidoResponse> ObterPedidoResponse(PedidoEntity pedido, List<PedidoProdutoEntity> pedidoProduto) 
        {

            var pedidoResponse = new PedidoResponse()
            {
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                CodigoPedido = pedido.CodigoPedido,
                DataFormatada = pedido.DataFormatada,
                DataPedido = pedido.DataPedido,
                NomeCliente = pedido.Cliente.Nome,
                StatusPedidoDescricao = pedido.StatusPedido.Descricao,
            };

            foreach (var item in pedidoProduto)
            {
                var pedProd = new PedidoProdutoResponse()
                {
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Produto = (ProdutoPresenter.ObterProdutoResponse(item.Produto)).Resultado!.FirstOrDefault()!,
                };

                pedidoResponse.ProdutosSelecionados.Add(pedProd);
            }

            var response = new ResponseBase<PedidoResponse>()
            {
                Sucesso = true,
                Mensagem = "Pedido encontrado com sucesso!",
                Resultado = [pedidoResponse]
            };

            return response;
        }
    }
}
