using Burguer404.Domain.Arguments.Webhook;

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
