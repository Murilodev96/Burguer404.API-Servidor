using System.Text.Json.Serialization;

namespace Burguer404.Domain.Arguments.Webhook
{
    public class NotificacaoWebhook
    {
        [JsonPropertyName("resource")]
        public string Resource { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        
    }  
}
