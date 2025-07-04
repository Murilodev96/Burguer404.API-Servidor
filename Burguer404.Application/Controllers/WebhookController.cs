using Burguer404.Application.Gateways;
using Burguer404.Application.UseCases.Webhook;
using Burguer404.Domain.Arguments.Webhook;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Infrastructure.Pagamentos.Operacoes;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Burguer404.Application.Controllers
{
    public class WebhookController
    {
        private IRepositoryPedido _repository;
        private IConfiguration _config;
        private readonly HttpClient _clienteHttp;

        public WebhookController(IRepositoryPedido repository, IConfiguration config, HttpClient clienteHttp)
        {
            _repository = repository;
            _config = config;
            _clienteHttp = clienteHttp;
        }

        public async Task ConsultarPagamento(NotificacaoWebhook notificacao)
        {
            var useCase = ValidarNotificacaoUseCase.Create();
            await useCase.ExecuteAsync(notificacao);

            var consultarPagamentoCompleto = ConsultarPagamentoCompletoMercadoPago.Create(_clienteHttp, _config);
            var (codigoPedido, status) = await consultarPagamentoCompleto.ConsultarPagamentoMercadoPago(notificacao);

            var pedidoGateway = new PedidosGateway(_repository);
            var useCaseAttPedido = AtualizarPagamentoPedidoUseCase.Create(pedidoGateway);

            var pagamentoAtualizado = await useCaseAttPedido.ExecuteAsync(codigoPedido, status);
        }

        public bool ValidarAssinaturaWebhook(string assinatura, string requestId, string dataId)
        {
            var secret = _config["TokenQrCodeMercadoPago"];
            try
            {
                var partes = assinatura.Split(",");
                string timestamp = null;
                string hashRecebido = null;

                foreach (var parte in partes)
                {
                    var keyValue = parte.Split("=");
                    if (keyValue.Length == 2)
                    {
                        var chave = keyValue[0].Trim();
                        var valor = keyValue[1].Trim();

                        if (chave == "ts")
                            timestamp = valor;
                        else if (chave == "v1")
                            hashRecebido = valor;
                    }
                }

                if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(hashRecebido))
                    return false;

                var manifestoAssinatura = $"id:{dataId};request-id:{requestId};ts:{timestamp};";

                var hashCalculado = HMACSHA256Hash(manifestoAssinatura, secret);

                return hashCalculado == hashRecebido;
            }
            catch
            {
                return false;
            }
        }

        private string HMACSHA256Hash(string texto, string chaveSecreta)
        {
            using (var hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(chaveSecreta)))
            {
                var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(texto));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
