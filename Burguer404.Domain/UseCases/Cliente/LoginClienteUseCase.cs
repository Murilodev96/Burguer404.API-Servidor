using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Domain.UseCases.Cliente
{
    public sealed class LoginClienteUseCase
    {
        private readonly IRepositoryCliente _clienteRepository;

        public LoginClienteUseCase( IRepositoryCliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteEntity> ExecuteAsync(string cpf)
        {
            var validacoes = ValidarCliente.Validar_LoginCliente_Request(cpf);

            if (!validacoes.Sucesso)
            {
                return new ClienteEntity();
            }

            var cliente = await _clienteRepository.ObterClientePorCpf(cpf);
            validacoes = ValidarCliente.Validar_ExistenciaCliente(cliente, cpf);

            if (!validacoes.Sucesso)
            {
                return new ClienteEntity();
            }

            return cliente;
        }
    }
}
