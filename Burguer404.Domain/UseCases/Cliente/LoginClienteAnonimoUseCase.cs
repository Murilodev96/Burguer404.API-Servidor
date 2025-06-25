using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;

namespace Burguer404.Domain.UseCases.Cliente
{
    public sealed class LoginClienteAnonimoUseCase
    {
        private readonly IRepositoryCliente _clienteRepository;

        public LoginClienteAnonimoUseCase(IRepositoryCliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteEntity> ExecuteAsync() =>
            await _clienteRepository.CadastrarClienteAnonimo();
        
    }
}
