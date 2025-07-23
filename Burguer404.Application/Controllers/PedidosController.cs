using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Infrastructure.Pagamentos.Operacoes;
using Microsoft.Extensions.Configuration;

namespace Burguer404.Application.Controllers
{
    public class PedidosController
    {
        private readonly IPedidosGateway _gateway;
        private readonly IConfiguration _config;
        private readonly IProdutoGateway _prodGateway;

        public PedidosController(IPedidosGateway gateway, IConfiguration config, IProdutoGateway prodGateway)
        {
            _gateway = gateway;
            _config = config;
            _prodGateway = prodGateway;
        }

        public async Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request) 
        {
            var useCase = new CadastrarPedidoUseCase(_gateway);
            var pedidoCadastrado = await useCase.ExecuteAsync(request);

            return new ResponseBase<string>() { Sucesso = pedidoCadastrado != string.Empty, Mensagem = pedidoCadastrado, Resultado = [pedidoCadastrado] };
        }

        public async Task<ResponseBase<PedidoResponse>> ListarPedidos(int clienteLogadoId) 
        {
            var useCase = new ListarPedidoUseCase(_gateway);
            var pedidos = await useCase.ExecuteAsync(clienteLogadoId);
        
            if(pedidos == null)
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };

            return PedidosPresenter.ObterListaPedidoResponse(pedidos);
        }

        public async Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo) 
        {
            var useCase = new VisualizarPedidoUseCase(_gateway, _prodGateway);
            var (pedido, pedidoProdutos) = await useCase.ExecuteAsync(codigo);

            if (pedido == null)
            {
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };
            }

            return PedidosPresenter.ObterPedidoResponse(pedido, pedidoProdutos!);
        }

        public async Task<ResponseBase<bool>> AvancarStatusPedido(string codigo) 
        {
            var useCase = new AvancarStatusPedidoUseCase(_gateway);
            var (sucesso, mensagem) = await useCase.ExecuteAsync(codigo);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<bool>> CancelarPedido(int pedidoId) 
        {
            var useCase = new CancelarPedidoUseCase(_gateway);
            var sucesso = await useCase.ExecuteAsync(pedidoId);

            if (!sucesso)
            {
                return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Ocorreu um erro ao tentar cancelar o pedido informado!", Resultado = [sucesso] };
            }

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Pedido cancelado com sucesso!", Resultado = [sucesso] };
        }

        public async Task<ResponseBase<string>> GerarQrCode(List<PagamentoRequest> itens) 
        {
            var useCase = new GerarQrCodeUseCase(_gateway, _prodGateway);
            var qrCodeRequest = await useCase.ExecuteAsync(itens);

            if (!(qrCodeRequest is QrCodeRequest))
                return new ResponseBase<string>() { Sucesso = false, Mensagem = "Ocorreu um erro com os dados do pedido", Resultado = [] };

            var mercadoPago = SolicitarPagamentoMercadoPago.Create(_config);
            var (sucesso, qrCode) = await mercadoPago.SolicitarQrCodeMercadoPago(qrCodeRequest);

            if (!sucesso)
                return new ResponseBase<string>() { Sucesso = false, Mensagem = "Ocorreu um erro ao tentar gerar o QrCode com o mercado pago", Resultado = [] };

            return new ResponseBase<string>() { Sucesso = sucesso, Mensagem = "QrCode gerado com sucesso", Resultado = [qrCode] };
        }
    }
}
