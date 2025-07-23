using Burguer404.Domain.Arguments.Pedido;

namespace Burguer404.Domain.Ports.Repositories.Pedido
{
    public interface IRepositoryMercadoPago
    {
        Task<(bool, string)> SolicitarQrCodeMercadoPago(QrCodeRequest qrCodeRequest);
    }
}
