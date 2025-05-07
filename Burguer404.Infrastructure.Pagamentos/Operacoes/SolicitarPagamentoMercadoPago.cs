using Burguer404.Domain.Arguments.Pedido;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Burguer404.Infrastructure.Pagamentos.Operacoes
{
    public class SolicitarPagamentoMercadoPago : IRepositoryMercadoPago
    {
        private readonly IConfiguration _configuration;

        public SolicitarPagamentoMercadoPago(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(bool, string)> SolicitarQrCodeMercadoPago(QrCodeRequest qrCodeRequest)
        {
            var url = _configuration["UrlBaseQrCodeMercadoPago"];

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["TokenQrCodeMercadoPago"]);

            var content = new StringContent(JsonConvert.SerializeObject(qrCodeRequest), Encoding.UTF8, "application/json");

            var responseMercadoPago = await httpClient.PostAsync(url, content);

            responseMercadoPago.EnsureSuccessStatusCode();
            var jsonString = await responseMercadoPago.Content.ReadAsStringAsync();
            var qrCode = JsonConvert.DeserializeObject<QrCodeResponse>(jsonString);

            if (string.IsNullOrWhiteSpace(qrCode?.qr_data) || string.IsNullOrWhiteSpace(qrCode?.qr_data))
                return (false, "Ocorreu um erro ao tentar gerar o QR Code, tente realizar o pedido novamente!");
            
            return (true, qrCode.qr_data);
        }
    }
}
