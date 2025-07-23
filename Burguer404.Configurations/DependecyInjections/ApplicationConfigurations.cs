using Burguer404.Application.Gateways;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Application.UseCases.Pedido;
using Burguer404.Application.UseCases.Webhook;
using Microsoft.Extensions.DependencyInjection;

namespace Burguer404.Configurations.DependecyInjections
{
    public static class ApplicationConfigurations
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            // Register Gateways
            services.AddScoped<IPedidosGateway, PedidosGateway>();

            // Register Use Cases
            services.AddScoped<AvancarStatusPedidoUseCase>();
            services.AddScoped<CadastrarPedidoUseCase>();
            services.AddScoped<CancelarPedidoUseCase>();
            services.AddScoped<GerarQrCodeUseCase>();
            services.AddScoped<ListarPedidoUseCase>();
            services.AddScoped<VisualizarPedidoUseCase>();
            services.AddScoped<AtualizarPagamentoPedidoUseCase>();
            services.AddScoped<ValidarNotificacaoUseCase>();

            // Register Controllers
            services.AddScoped<Burguer404.Application.Controllers.PedidosController>();
            services.AddScoped<Burguer404.Application.Controllers.WebhookController>();

            return services;
        }
    }
}