using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Domain.Ports.Repositories.Cliente
{
    public interface IRepositoryCliente
    {
        Task<ClienteEntity> CadastrarCliente(ClienteEntity cliente);
        Task<List<ClienteEntity>> ListarClientes();
        Task<ClienteEntity?> ObterClientePorCpf(string cpf);
        Task<bool> ValidarCadastroCliente(string cpf, string email);
        Task<bool> AlterarStatusCliente(int clienteId);
    }
}
