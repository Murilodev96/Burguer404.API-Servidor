using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Domain.UseCases.Pedido;
using Burguer404.Domain.Validators.Pedido;

namespace Burguer404.Application.UseCases.Pedido
{
    public sealed class GerarQrCodeUseCase
    {

        private readonly IRepositoryProduto _produtoRepository;
        private readonly IRepositoryMercadoPago _mercadoPagoRepository;
        private readonly CadastrarPedidoUseCase _cadastrarPedidoUseCase;

        public GerarQrCodeUseCase(IRepositoryProduto produtoRepository,
                                  IRepositoryMercadoPago mercadoPagoRepository,
                                  CadastrarPedidoUseCase cadastrarPedidoUseCase)
        {
            _produtoRepository = produtoRepository;
            _mercadoPagoRepository = mercadoPagoRepository;
            _cadastrarPedidoUseCase = cadastrarPedidoUseCase;
        }

        public async Task<QrCodeRequest> ExecuteAsync(List<PagamentoRequest> itens, string codigo)
        {
            try
            {
                var total = Math.Round(itens.Sum(x => x.Valor), 2);
                var qrReq = new QrCodeRequest
                {
                    description = "Lanchonete Burguer404",
                    total_amount = total,
                    title = $"Confirmação de pagamento do pedido {codigo}",
                    external_reference = codigo,
                    items = []
                };

                foreach (var item in itens)
                {
                    // Busca dados dos produtos
                    var lanche = await _produtoRepository.ObterProdutoPorId(item.LancheId);
                    var acompanhamento = await _produtoRepository.ObterProdutoPorId(item.AcompanhamentoId);
                    var bebida = await _produtoRepository.ObterProdutoPorId(item.BebidaId);
                    var sobremesa = await _produtoRepository.ObterProdutoPorId(item.SobremesaId);

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