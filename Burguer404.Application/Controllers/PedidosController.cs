using Burguer404.Application.Arguments.Pedido;
using Burguer404.Application.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;

namespace Burguer404.Application.Controllers
{
    public class PedidosController
    {
        private IRepositoryPedido _repository;
        private IRepositoryProduto _produtoRepository;
        private IRepositoryMercadoPago _mercadoPago;

        public PedidosController(IRepositoryPedido repository, IRepositoryProduto produtoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var useCase = CadastrarPedidoUseCase.Create(pedidoGateway);

            var pedidoCadastrado = await useCase.ExecuteAsync(request);

            return new ResponseBase<string>() { Sucesso = pedidoCadastrado != string.Empty, Mensagem = pedidoCadastrado, Resultado = [pedidoCadastrado] };
        }

        public async Task<ResponseBase<PedidoResponse>> ListarPedidos(int clienteLogadoId) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var useCase = ListarPedidosUseCase.Create(pedidoGateway);

            var pedidos = await useCase.ExecuteAsync(clienteLogadoId);
        
            if(pedidos == null)
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };

            return PedidosPresenter.ObterListaPedidoResponse(pedidos);
        }

        public async Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var produtoGateway = new ProdutoGateway(_produtoRepository);

            var useCase = VisualizarPedidoUseCase.Create(pedidoGateway, produtoGateway);

            var (pedido, pedidoProdutos) = await useCase.ExecuteAsync(codigo);

            if (pedido == null)
            {
                return new ResponseBase<PedidoResponse>() { Sucesso = false, Mensagem = "Não foi encontrado produtos para este cliente", Resultado = [] };
            }

            return PedidosPresenter.ObterPedidoResponse(pedido, pedidoProdutos!);
        }

        public async Task<ResponseBase<bool>> AvancarStatusPedido(string codigo) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var useCase = AvancarStatusPedidoUseCase.Create(pedidoGateway);

            var (sucesso, mensagem) = await useCase.ExecuteAsync(codigo);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<bool>> CancelarPedido(int pedidoId) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var useCase = CancelarPedidoUseCase.Create(pedidoGateway);

            var sucesso = await useCase.ExecuteAsync(pedidoId);

            if (!sucesso)
            {
                return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Ocorreu um erro ao tentar cancelar o pedido informado!", Resultado = [sucesso] };
            }

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = "Pedido cancelado com sucesso!", Resultado = [sucesso] };
        }

        public async Task<ResponseBase<string>> GerarQrCode(List<PagamentoRequest> itens) 
        {
            var pedidoGateway = new PedidosGateway(_repository);
            var produtoGateway = new ProdutoGateway(_produtoRepository);

            var useCase = GerarQrCodeUseCase.Create(pedidoGateway, produtoGateway);

            var qrCodeRequest = await useCase.ExecuteAsync(itens);

            if (!(qrCodeRequest is QrCodeRequest))
                return new ResponseBase<string>() { Sucesso = false, Mensagem = "Ocorreu um erro com os dados do pedido", Resultado = [] };

            var (sucesso, qrCode) = await _mercadoPago.SolicitarQrCodeMercadoPago(qrCodeRequest);

            if (!sucesso)
                return new ResponseBase<string>() { Sucesso = false, Mensagem = "Ocorreu um erro ao tentar gerar o QrCode com o mercado pago", Resultado = [] };

            return new ResponseBase<string>() { Sucesso = sucesso, Mensagem = "QrCode gerado com sucesso", Resultado = [qrCode] };
        }
    }
}
