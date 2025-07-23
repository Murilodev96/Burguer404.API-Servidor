using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public class GerarQrCodeUseCase
    {

        private readonly IPedidosGateway _pedidoGateway;
        private readonly IProdutoGateway _produtoGateway;

        public GerarQrCodeUseCase(IPedidosGateway pedidoGateway, IProdutoGateway produtoGateway)
        {
            _pedidoGateway = pedidoGateway;
            _produtoGateway = produtoGateway;
        }

        public static GerarQrCodeUseCase Create(PedidosGateway pedidoGateway, IProdutoGateway produtoGateway)
        {
            return new GerarQrCodeUseCase(pedidoGateway, produtoGateway);
        }

        public async Task<QrCodeRequest?> ExecuteAsync(List<PagamentoRequest> itens)
        {
            var validacoes = ValidarPedido.Validar_GerarQrCode_Request(itens);

            if (!validacoes.Sucesso)
                return null;

            List<int> itensPedido = new List<int>();
            Parallel.ForEach(itens, item =>
            {
                if (item.LancheId > 0)
                    itensPedido.Add(item.LancheId);
                if (item.AcompanhamentoId > 0)
                    itensPedido.Add(item.AcompanhamentoId);
                if (item.BebidaId > 0)
                    itensPedido.Add(item.BebidaId);
                if (item.SobremesaId > 0)
                    itensPedido.Add(item.SobremesaId);
            });

            var pedidoRequest = new PedidoRequest()
            {
                ClienteId = itens.FirstOrDefault()!.ClienteId,
                ProdutosSelecionados = itensPedido
            };

            var pedido = PedidoEntity.MapPedido(pedidoRequest);
            
            if (!(pedido is PedidoEntity))
                return null;

            var codigoPedido = await _pedidoGateway.CriarPedidoAsync(pedido);

            pedido.PedidoProduto = [.. pedido.ProdutosSelecionados
                                                .GroupBy(produtoId => produtoId)
                                                .Select(grupo => new PedidoProdutoEntity
                                                {
                                                    PedidoId = pedido.Id,
                                                    ProdutoId = grupo.Key,
                                                    Quantidade = grupo.Count()
                                                })];
            
            await _pedidoGateway.InserirProdutosNoPedidoAsync([.. pedido.PedidoProduto]);
            

            try
            {
                var total = Math.Round(itens.Sum(x => x.Valor), 2);
                var qrReq = new QrCodeRequest
                {
                    description = "Lanchonete Burguer404",
                    total_amount = total,
                    title = $"Confirmação de pagamento do pedido {codigoPedido.CodigoPedido}",
                    notification_url = "https://4d162fda0278.ngrok-free.app/PagamentosWebhook/notificacao?source_news=webhooks",
                    external_reference = codigoPedido.CodigoPedido,
                    items = []
                };

                foreach (var item in itens)
                {
                    // Busca dados dos produtos
                    var lanche = await _produtoGateway.ObterProdutoPorIdAsync(item.LancheId);
                    var acompanhamento = await _produtoGateway.ObterProdutoPorIdAsync(item.AcompanhamentoId);
                    var bebida = await _produtoGateway.ObterProdutoPorIdAsync(item.BebidaId);
                    var sobremesa = await _produtoGateway.ObterProdutoPorIdAsync(item.SobremesaId);

                    qrReq.items.Add(new ItemQrCode
                    {
                        title = $"Lanche: {lanche?.Nome} - Acompanhamento: {acompanhamento?.Nome} - Bebida: {bebida?.Nome} - Sobremesa: {sobremesa?.Nome}",
                        description = "Combo solicitado via app no Burguer404",
                        quantity = 1,
                        total_amount = item.Valor,
                        unit_price = item.Valor,
                        category = "Lanche",
                        sku_number = "001",
                        unit_measure = "unit"
                    });
                }

                return qrReq;
            }
            catch (Exception ex)
            {
                return new QrCodeRequest();
            }
        }
    }
}