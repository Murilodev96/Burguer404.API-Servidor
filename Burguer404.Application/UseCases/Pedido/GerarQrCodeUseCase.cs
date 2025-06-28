using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Entities.Pedido;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public  class GerarQrCodeUseCase
    {

        private readonly PedidosGateway _pedidoGateway;
        private readonly ProdutoGateway _produtoGateway;

        public GerarQrCodeUseCase(PedidosGateway pedidoGateway, ProdutoGateway produtoGateway)
        {
            _pedidoGateway = pedidoGateway;
            _produtoGateway = produtoGateway;
        }

        public static GerarQrCodeUseCase Create(PedidosGateway pedidoGateway, ProdutoGateway produtoGateway)
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
            try
            {
                var total = Math.Round(itens.Sum(x => x.Valor), 2);
                var qrReq = new QrCodeRequest
                {
                    description = "Lanchonete Burguer404",
                    total_amount = total,
                    title = $"Confirmação de pagamento do pedido {codigoPedido.CodigoPedido}",
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
                        total_amount = total,
                        unit_price = total,
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