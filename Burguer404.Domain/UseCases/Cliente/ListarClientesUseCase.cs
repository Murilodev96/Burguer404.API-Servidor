using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;

namespace Burguer404.Domain.UseCases.Cliente
{
    public sealed class ListarClientesUseCase
    {
        private readonly IRepositoryCliente _clienteRepository;

        public ListarClientesUseCase(IRepositoryCliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteEntity>> ExecuteAsync() =>

            await _clienteRepository.ListarClientes();

    }
}
