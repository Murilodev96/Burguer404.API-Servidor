using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burguer404.Application.Gateways
{
    public class ClienteGateway
    {
        private IRepositoryCliente _repository;

        public ClienteGateway(IRepositoryCliente repository)
        {
            _repository = repository;
        }

        public async Task<ClienteEntity> ObterClientePorCpfAsync(string cpf)
            => await _repository.ObterClientePorCpf(cpf);

        public async Task<ClienteEntity> CadastrarClienteAsync(ClienteEntity cliente)
            => await _repository.CadastrarCliente(cliente);

        public async Task<List<ClienteEntity>> ListarClientesAsync()
            => await _repository.ListarClientes();

        public async Task<bool> AlterarStatusClienteAsync(int clienteId)
            => await _repository.AlterarStatusCliente(clienteId);

        public async Task<ClienteEntity> CadastrarClienteAnonimoAsync()
            => await _repository.CadastrarClienteAnonimo();
    }
}
