using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Ports.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Cliente;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Application.Controllers
{
    public class ClienteController
    {
        private IClienteGateway _gateway;

        public ClienteController(IClienteGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<ResponseBase<ClienteResponse>> LoginCliente(string cpf) 
        {
            var useCase = LoginClienteUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync(cpf);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Cliente não encontrado!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> CadastrarCliente(ClienteRequest request) 
        {
            var useCase = CadastrarClienteUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync(request);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> ListarClientes() 
        {
            var useCase = ListarClientesUseCase.Create(_gateway);

            var clientes = await useCase.ExecuteAsync();

            return ClientePresenter.ObterListaClienteResponse(clientes);
        }

        public async Task<ResponseBase<bool>> AlterarStatusCliente(int clienteId) 
        {
            var useCase = AlterarStatusClienteUseCase.Create(_gateway);

            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<ClienteResponse>> LoginClienteAnonimo() 
        {
            var useCase = LoginClienteAnonimoUseCase.Create(_gateway);

            var cliente = await useCase.ExecuteAsync();

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }
    }
}
