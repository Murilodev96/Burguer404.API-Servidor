using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class LoginClienteAnonimoUseCase
    {
        private readonly ClienteGateway _clienteGateway;

        public LoginClienteAnonimoUseCase(ClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static LoginClienteAnonimoUseCase Create(ClienteGateway clienteGateway)
        {
            return new LoginClienteAnonimoUseCase(clienteGateway);
        }

        public async Task<ClienteEntity> ExecuteAsync() =>
            await _clienteGateway.CadastrarClienteAnonimoAsync();
        
    }
}
