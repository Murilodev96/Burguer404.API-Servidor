using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Arguments.Pedido;

namespace Burguer404.Domain.Ports.Services.Pedido
{
    public interface IServicePedido
    {
        Task<ResponseBase<string>> CadastrarPedido(PedidoRequest request);
        Task<ResponseBase<PedidoResponse>> ListarPedidos();
        Task<ResponseBase<bool>> CancelarPedido(int pedidoId);
        Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo);
        Task<ResponseBase<bool>> AvancarStatusPedido(string codigo);
        Task<ResponseBase<bool>> gerarQrCode(List<PagamentoRequest> request);
    }
}
