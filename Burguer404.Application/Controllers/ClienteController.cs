using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Gateways;
using Burguer404.Application.Presenters;
using Burguer404.Application.UseCases.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Repositories.Cliente;

namespace Burguer404.Application.Controllers
{
    public class ClienteController
    {
        private IRepositoryCliente _repository;

        public ClienteController(IRepositoryCliente repository)
        {
            _repository = repository;
        }

        public async Task<ResponseBase<ClienteResponse>> LoginCliente(string cpf) 
        {
            var clienteGateway = new ClienteGateway(_repository);
            var useCase = LoginClienteUseCase.Create(clienteGateway);

            var cliente = await useCase.ExecuteAsync(cpf);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Cliente não encontrado!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> CadastrarCliente(ClienteRequest request) 
        {
            var clienteGateway = new ClienteGateway(_repository);
            var useCase = CadastrarClienteUseCase.Create(clienteGateway);

            var cliente = await useCase.ExecuteAsync(request);

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }

        public async Task<ResponseBase<ClienteResponse>> ListarClientes() 
        {
            var clienteGateway = new ClienteGateway(_repository);
            var useCase = ListarClientesUseCase.Create(clienteGateway);

            var clientes = await useCase.ExecuteAsync();

            return ClientePresenter.ObterListaClienteResponse(clientes);
        }

        public async Task<ResponseBase<bool>> AlterarStatusCliente(int clienteId) 
        {
            var clienteGateway = new ClienteGateway(_repository);
            var useCase = AlterarStatusClienteUseCase.Create(clienteGateway);

            var (sucesso, mensagem) = await useCase.ExecuteAsync(clienteId);

            return new ResponseBase<bool>() { Sucesso = sucesso, Mensagem = mensagem, Resultado = [sucesso] };
        }

        public async Task<ResponseBase<ClienteResponse>> LoginClienteAnonimo() 
        {
            var clienteGateway = new ClienteGateway(_repository);
            var useCase = LoginClienteAnonimoUseCase.Create(clienteGateway);

            var cliente = await useCase.ExecuteAsync();

            if (cliente == null)
            {
                return new ResponseBase<ClienteResponse>() { Sucesso = false, Mensagem = "Ocorreu um erro durante o cadastro do cliente!", Resultado = [] };
            }

            return ClientePresenter.ObterClienteResponse(cliente);
        }
    }
}
