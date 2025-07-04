using Burguer404.Application.Gateways;
using Burguer404.Domain.Arguments.Webhook;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Burguer404.Application.UseCases.Webhook
{
    public class ValidarNotificacaoUseCase
    {

        public ValidarNotificacaoUseCase() { }

        public static ValidarNotificacaoUseCase Create()
        {
            return new ValidarNotificacaoUseCase();
        }

        public async Task ExecuteAsync(NotificacaoWebhook notificacao)
        {
            if (notificacao == null || string.IsNullOrEmpty(notificacao.Type))
            {
                throw new ArgumentException("Payload do webhook inválido");
            }

            if (notificacao.Type.ToLower() != "payment")
            {
                throw new ArgumentException("Evento não processado - Tipo diferente de pagamento");
            }            
        }
    }
}
