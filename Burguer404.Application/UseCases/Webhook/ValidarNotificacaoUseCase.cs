using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Arguments.Webhook;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Burguer404.Application.UseCases.Webhook
{
    public class ValidarNotificacaoUseCase
    {
        private readonly IPedidosGateway _pedidosGateway;

        public ValidarNotificacaoUseCase(IPedidosGateway pedidosGateway)
        {
            _pedidosGateway = pedidosGateway;
        }

        public static ValidarNotificacaoUseCase Create(IPedidosGateway pedidosGateway)
        {
            return new ValidarNotificacaoUseCase(pedidosGateway);
        }

        public async Task ExecuteAsync(NotificacaoWebhook notificacao)
        {
            if (notificacao == null || string.IsNullOrEmpty(notificacao.type))
            {
                throw new ArgumentException("Payload do webhook inválido");
            }

            if (notificacao.type.ToLower() != "payment")
            {
                throw new ArgumentException("Evento não processado - Tipo diferente de pagamento");
            }            
        }
    }
}
