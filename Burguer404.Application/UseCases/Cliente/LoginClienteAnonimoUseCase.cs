using Burguer404.Application.Ports.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class LoginClienteAnonimoUseCase
    {
        private readonly IClienteGateway _clienteGateway;

        public LoginClienteAnonimoUseCase(IClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static LoginClienteAnonimoUseCase Create(IClienteGateway clienteGateway)
        {
            return new LoginClienteAnonimoUseCase(clienteGateway);
        }

        public async Task<ClienteEntity> ExecuteAsync()
        {
            return await _clienteGateway.CadastrarClienteAnonimoAsync();
        }
    }
}
