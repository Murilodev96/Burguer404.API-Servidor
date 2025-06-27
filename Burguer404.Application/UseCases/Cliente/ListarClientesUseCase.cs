using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class ListarClientesUseCase
    {
        private readonly ClienteGateway _clienteGateway;

        public ListarClientesUseCase(ClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static ListarClientesUseCase Create(ClienteGateway clienteGateway)
        {
            return new ListarClientesUseCase(clienteGateway);
        }

        public async Task<List<ClienteEntity>> ExecuteAsync()
            => await _clienteGateway.ListarClientesAsync();

    }
}
