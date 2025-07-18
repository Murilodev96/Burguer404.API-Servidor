using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class AlterarStatusClienteUseCase
    {
        private readonly IClienteGateway _clienteGateway;

        public AlterarStatusClienteUseCase(IClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static AlterarStatusClienteUseCase Create(IClienteGateway clienteGateway)
        {
            return new AlterarStatusClienteUseCase(clienteGateway);
        }

        public async Task<(bool sucesso, string mensagem)> ExecuteAsync(int clienteId)
        {
            var clienteAlterado = await _clienteGateway.AlterarStatusClienteAsync(clienteId);

            if (!clienteAlterado)
            {
                return (false, "Cliente não encontrado!");
            }

            return (true, "Status do cliente alterado com sucesso!");
        }
    }
}
