using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;

namespace Burguer404.Domain.UseCases.Cliente
{
    public sealed class CadastrarClienteUseCase
    {
        private readonly IRepositoryCliente _clienteRepository;

        public CadastrarClienteUseCase(IRepositoryCliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteEntity> ExecuteAsync(ClienteEntity request)
        {
            request.Status = true;           
            return await _clienteRepository.CadastrarCliente(request);

        }
    }
}
