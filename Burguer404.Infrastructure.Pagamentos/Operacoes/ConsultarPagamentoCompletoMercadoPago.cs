using Burguer404.Domain.Arguments.Webhook;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Burguer404.Infrastructure.Pagamentos.Operacoes
{
    public class ConsultarPagamentoCompletoMercadoPago
    {
        private readonly HttpClient _clienteHttp;
        private readonly IConfiguration _configuration;
        private readonly string _tokenAcesso;

        public ConsultarPagamentoCompletoMercadoPago(HttpClient clienteHttp, IConfiguration configuration)
        {
            _clienteHttp = clienteHttp;
            _configuration = configuration;
            _tokenAcesso = _configuration["TokenQrCodeMercadoPago"]!.ToString();
            _clienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenAcesso);
        }

        public static ConsultarPagamentoCompletoMercadoPago Create(HttpClient clienteHttp, IConfiguration configuration)
        {
            return new ConsultarPagamentoCompletoMercadoPago(clienteHttp, configuration);
        }

        public async Task<(string, string)> ConsultarPagamentoMercadoPago(NotificacaoWebhook notificacao)
        {
            try
            {
                var respostaPagamento = await _clienteHttp.GetAsync($"{_configuration["UrlConsultarPagamentoCompletoMercadoPago"]}{notificacao.Data.Id}");
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
