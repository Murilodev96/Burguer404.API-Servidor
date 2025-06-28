using Burguer404.Application.Gateways;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public  class LoginClienteUseCase
    {
        private ClienteGateway _clienteGateway;

        public LoginClienteUseCase(ClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static LoginClienteUseCase Create(ClienteGateway clienteGateway) 
        {
            return new LoginClienteUseCase(clienteGateway);
        }

        public async Task<ClienteEntity?> ExecuteAsync(string cpf)
        {
            var validacoes = ValidarCliente.Validar_LoginCliente_Request(cpf);

            if (!validacoes.Sucesso)
                return null;

            var cliente = await _clienteGateway.ObterClientePorCpfAsync(cpf);
            validacoes = ValidarCliente.Validar_ExistenciaCliente(cliente, cpf);

            if (!validacoes.Sucesso)
                return null;

            return cliente;
        }
    }
}
