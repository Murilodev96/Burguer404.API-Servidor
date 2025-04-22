using Burguer404.Application.Arguments.Pedido;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Domain.Ports.Services.Pedido
{
    public interface IServicePedido
    {
        Task<ResponseBase<bool>> CadastrarPedido(PedidoRequest request);
        Task<ResponseBase<PedidoResponse>> ListarPedidos();
        Task<ResponseBase<bool>> CancelarPedido(int pedidoId);
        Task<ResponseBase<PedidoResponse>> VisualizarPedido(string codigo);
    }
}
