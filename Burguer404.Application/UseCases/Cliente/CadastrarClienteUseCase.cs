using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Interfaces.Gateways;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public class CadastrarClienteUseCase
    {
        private readonly IClienteGateway _clienteGateway;

        public CadastrarClienteUseCase(IClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static CadastrarClienteUseCase Create(IClienteGateway clienteGateway)
        {
            return new CadastrarClienteUseCase(clienteGateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync(ClienteRequest request)
        {
            var validacoes = ValidarCliente.Validar_CadastroCliente_Request(request);

            if (!validacoes.Sucesso)
            {
                return null;
            }

            var clienteExiste = await _clienteGateway.ValidarCadastroClienteAsync(request.Cpf, request.Email);

            var cliente = new ClienteEntity();

            return await _clienteGateway.CadastrarClienteAsync(cliente);
        }
    }
}
