using Burguer404.Domain.Arguments.AuthAzureAd;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Burguer404.Infrastructure.Auth.Autenticacao
{
    public class AutenticacaoAzureAd
    {
        private readonly IConfiguration _configuration;

        public AutenticacaoAzureAd(IConfiguration configuration) => _configuration = configuration;

        public static AutenticacaoAzureAd Create(IConfiguration configuration)
        {
            return new AutenticacaoAzureAd(configuration);
        }

        public async Task<AuthResponse?> AutenticarComAzureAd(string cpf)
        {
            try
            {
                using var httpClient = new HttpClient();

                var body = new AuthRequest { cpf = cpf.Replace(".", "").Replace("-", "") };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var respostaAzure = await httpClient.PostAsync($"{_configuration["UrlAutenticacaoAzureAd"]}", content);
                respostaAzure.EnsureSuccessStatusCode();

                var jsonPagamento = await respostaAzure.Content.ReadAsStringAsync();
                var autenticacao = JsonConvert.DeserializeObject<AuthResponse>(jsonPagamento);

                return autenticacao;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro de autenticação com o Azure Ad: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar webhook: {ex.Message}");
            }
        }
    }
}
