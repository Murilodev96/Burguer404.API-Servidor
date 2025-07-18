using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Infrastructure.Pagamentos.Operacoes;
using Microsoft.Extensions.Configuration;

namespace Burguer404.Application.Controllers
{
    public class PedidosController
    {
        private readonly CadastrarPedidoUseCase _cadastrarPedidoUseCase;
        private readonly ListarPedidoUseCase _listarPedidoUseCase;
        private readonly VisualizarPedidoUseCase _visualizarPedidoUseCase;
        private readonly AvancarStatusPedidoUseCase _avancarStatusPedidoUseCase;
        private readonly CancelarPedidoUseCase _cancelarPedidoUseCase;
        private readonly GerarQrCodeUseCase _gerarQrCodeUseCase;
        private readonly IConfiguration _config;

        public PedidosController(
            CadastrarPedidoUseCase cadastrarPedidoUseCase,
            ListarPedidoUseCase listarPedidoUseCase,
            VisualizarPedidoUseCase visualizarPedidoUseCase,
            AvancarStatusPedidoUseCase avancarStatusPedidoUseCase,
            CancelarPedidoUseCase cancelarPedidoUseCase,
            GerarQrCodeUseCase gerarQrCodeUseCase,
            IConfiguration config)
        {
            _cadastrarPedidoUseCase = cadastrarPedidoUseCase;
            _listarPedidoUseCase = listarPedidoUseCase;
            _visualizarPedidoUseCase = visualizarPedidoUseCase;
            _avancarStatusPedidoUseCase = avancarStatusPedidoUseCase;
            _cancelarPedidoUseCase = cancelarPedidoUseCase;
            _gerarQrCodeUseCase = gerarQrCodeUseCase;
            _config = config;
        }

        public async Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request) 
        {
            var pedidoCadastrado = await _cadastrarPedidoUseCase.ExecuteAsync(request);

            return new ResponseBase<string>() { Sucesso = pedidoCadastrado != string.Empty, Mensagem = pedidoCadastrado, Resultado = [pedidoCadastrado] };
        }

        public async Task<ResponseBase<PedidoResponse>> ListarPedidos(int clienteLogadoId) 
        {
            var pedidos = await _listarPedidoUseCase.ExecuteAsync(clienteLogadoId);
        
            if(pedidos == null)
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };

            return PedidosPresenter.ObterListaPedidoResponse(pedidos);
        }

        public async Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo) 
        {
            var (pedido, pedidoProdutos) = await _visualizarPedidoUseCase.ExecuteAsync(codigo);

            if (pedido == null)
            {
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };
            }

            return PedidosPresenter.ObterPedidoResponse(pedido, pedidoProdutos!);
        }

        public async Task<ResponseBase<bool>> AvancarStatusPedido(string codigo) 
        {
            var (sucesso, mensagem) = await _avancarStatusPedidoUseCase.ExecuteAsync(codigo);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<bool>> CancelarPedido(int pedidoId) 
        {
            var sucesso = await _cancelarPedidoUseCase.ExecuteAsync(pedidoId);

            if (!sucesso)
            {
                return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Ocorreu um erro ao tentar cancelar o pedido informado!", Resultado = [sucesso] };
            }

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Pedido cancelado com sucesso!", Resultado = [sucesso] };
        }

        public async Task<ResponseBase<string>> GerarQrCode(List<PagamentoRequest> itens) 
        {
            var qrCodeRequest = await _gerarQrCodeUseCase.ExecuteAsync(itens);

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
