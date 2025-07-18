using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Webhook;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Webhook
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosWebhook : Controller
    {
        private readonly WebhookController _webhookController;

        public PagamentosWebhook(IRepositoryPedido repositorio, IConfiguration config)
        {
            _webhookController = new WebhookController(repositorio, config);
        }

        [HttpPost("notificacao")]
        public async Task<IActionResult> ReceberWebhook([FromBody] NotificacaoWebhook notificacao)
        {
            try
            {
                await _webhookController.ConsultarPagamento(notificacao);
                return Ok("Webhook processado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
