using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class CadastrarClienteUseCase
    {
        private readonly ClienteGateway _clienteGateway;

        public CadastrarClienteUseCase(ClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static CadastrarClienteUseCase Create(ClienteGateway clienteGateway) 
        {
            return new CadastrarClienteUseCase(clienteGateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync(ClienteRequest request)
        {
            var cliente = ClienteEntity.MapCliente(request);
            if (!(cliente is ClienteEntity))
                return null;

            cliente.Status = true;           
            return await _clienteGateway.CadastrarClienteAsync(cliente);
        }
    }
}
