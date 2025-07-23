using Burguer404.Domain.Arguments.Webhook;

namespace Burguer404.Domain.Ports.Repositories.Pedido
{
    public interface IConsultaPagamentoMercadoPago
    {
        Task<(string, string)> ConsultarPagamentoMercadoPago(NotificacaoWebhook notificacao);
    }
}
