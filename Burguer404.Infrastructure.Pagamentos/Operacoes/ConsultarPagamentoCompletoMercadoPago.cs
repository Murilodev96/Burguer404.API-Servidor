using Burguer404.Domain.Arguments.Webhook;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Burguer404.Infrastructure.Pagamentos.Operacoes
{
    public class ConsultarPagamentoCompletoMercadoPago
    {
        private readonly IConfiguration _configuration;
        private readonly string _tokenAcesso;

        public ConsultarPagamentoCompletoMercadoPago(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenAcesso = _configuration["TokenQrCodeMercadoPago"]!.ToString();
        }

        public static ConsultarPagamentoCompletoMercadoPago Create(IConfiguration configuration)
        {
            return new ConsultarPagamentoCompletoMercadoPago(configuration);
        }

        public async Task<(string, string)> ConsultarPagamentoMercadoPago(NotificacaoWebhook notificacao)
        {
            try
            {
                using var httpClient = new HttpClient();
                //Para realizar a consulta do pagamento utilizando o ID do pagamento é necessário utilizar o TOKEN de PRD.
                //Token PRD:
                //APP_USR-3115951857672142-050508-a729943bacb0a24fb89dd9a701480ff4-171894340
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["TokenQrCodeMercadoPago"]);

                var respostaPagamento = await httpClient.GetAsync($"{_configuration["UrlConsultarPagamentoCompletoMercadoPago"]}{notificacao.Resource}");
                respostaPagamento.EnsureSuccessStatusCode();

                var jsonPagamento = await respostaPagamento.Content.ReadAsStringAsync();
                var pagamento = JsonSerializer.Deserialize<JsonElement>(jsonPagamento);

                var status = pagamento.GetProperty("status").GetString();
                var referenciaExterna = pagamento.GetProperty("external_reference").GetString();

                return (referenciaExterna, status)!;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao consultar API do Mercado Pago: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar webhook: {ex.Message}");
            }
        }
    }
}
