using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class ListarClientesUseCase
    {
        private readonly IClienteGateway _clienteGateway;

        public ListarClientesUseCase(IClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static ListarClientesUseCase Create(IClienteGateway clienteGateway)
        {
            return new ListarClientesUseCase(clienteGateway);
        }

        public async Task<List<ClienteEntity>> ExecuteAsync()
            => await _clienteGateway.ListarClientesAsync();
    }
}
