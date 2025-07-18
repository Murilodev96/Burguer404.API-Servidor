using Burguer404.Application.Gateways;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Application.UseCases.Webhook;
using Burguer404.Domain.Arguments.Webhook;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Infrastructure.Pagamentos.Operacoes;
using Microsoft.Extensions.Configuration;


namespace Burguer404.Application.Controllers
{
    public class WebhookController
    {
        private IRepositoryPedido _repository;
        private IConfiguration _config;
        private IPedidosGateway _pedidoGateway;

        public WebhookController(IRepositoryPedido repository, IConfiguration config)
        {
            _repository = repository;
            _pedidoGateway = new PedidosGateway(_repository);
            _config = config;
        }

        public async Task ConsultarPagamento(NotificacaoWebhook notificacao)
        {
            var useCase = ValidarNotificacaoUseCase.Create(_pedidoGateway);
            await useCase.ExecuteAsync(notificacao);

            var consultarPagamentoCompleto = ConsultarPagamentoCompletoMercadoPago.Create(_config);
            var (codigoPedido, status) = await consultarPagamentoCompleto.ConsultarPagamentoMercadoPago(notificacao);

            var pedidoGateway = new PedidosGateway(_repository);
            var useCaseAttPedido = AtualizarPagamentoPedidoUseCase.Create(pedidoGateway);

            var pagamentoAtualizado = await useCaseAttPedido.ExecuteAsync(codigoPedido, status);
        }

    }
}
