using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Webhook;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Burguer404.Api.Webhook
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosWebhook : Controller
    {
        private readonly WebhookController _webhookController;

        public PagamentosWebhook(IRepositoryPedido repositorio, IConfiguration config, HttpClient clienteHttp)
        {
            _webhookController = new WebhookController(repositorio, config, clienteHttp);
        }

        [HttpPost("notificacao")]
        public async Task<IActionResult> ReceberWebhook(
            [FromBody] NotificacaoWebhook notificacao,
            [FromHeader(Name = "x-assinatura")] string assinatura,
            [FromHeader(Name = "x-request-id")] string requestId)
        {
            if (string.IsNullOrEmpty(assinatura) || string.IsNullOrEmpty(requestId))
            {
                return BadRequest("Headers de assinatura não fornecidos");
            }

            if (!_webhookController.ValidarAssinaturaWebhook(assinatura, requestId, notificacao.Data.Id))
            {
                return Unauthorized("Assinatura do webhook inválida");
            }

            await _webhookController.ConsultarPagamento(notificacao);
            return Ok("Webhook processado com sucesso");
        }

        
    }
}
