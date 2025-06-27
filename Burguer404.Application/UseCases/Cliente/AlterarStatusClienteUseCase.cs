using Burguer404.Application.Gateways;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public sealed class AlterarStatusClienteUseCase
    {
        private readonly ClienteGateway _clienteGateway;

        public AlterarStatusClienteUseCase(ClienteGateway clienteGateway)
        {
            _clienteGateway = clienteGateway;
        }

        public static AlterarStatusClienteUseCase Create(ClienteGateway clienteGateway)
        {
            return new AlterarStatusClienteUseCase(clienteGateway);
        }

        public async Task<(bool, string)> ExecuteAsync(int clienteId)
        {
            var response = new ResponseBase<bool>();
            var validacoes = ValidarCliente.Validar_AlterarStatusCliente_Request(clienteId);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return (validacoes.Sucesso, validacoes.Mensagem);
            }

            var clienteAlterado = await _clienteGateway.AlterarStatusClienteAsync(clienteId);
            if (!clienteAlterado)
            {
                response.Mensagem = "Cliente informado não encontrado!";
                return (validacoes.Sucesso, validacoes.Mensagem);
            }

            response.Resultado = [clienteAlterado];
            response.Sucesso = true;
            response.Mensagem = "Status do cliente alterado com sucesso!";
            return (validacoes.Sucesso, validacoes.Mensagem);
        }
    }
}
