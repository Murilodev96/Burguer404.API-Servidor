using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.Ports.Gateways
{
    public interface IClienteGateway
    {
        Task<ClienteEntity?> ObterClientePorCpfAsync(string cpf);
        Task<ClienteEntity> CadastrarClienteAsync(ClienteEntity cliente);
        Task<List<ClienteEntity>> ListarClientesAsync();
        Task<bool> AlterarStatusClienteAsync(int clienteId);
        Task<ClienteEntity> CadastrarClienteAnonimoAsync();
    }
}