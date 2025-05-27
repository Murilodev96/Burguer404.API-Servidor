using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;

namespace Burguer404.Domain.Ports.Services.Cliente
{
    public interface IServiceCliente
    {
        Task<ResponseBase<ClienteResponse>> CadastrarCliente(ClienteRequest request);
        Task<ResponseBase<ClienteResponse>> ListarClientes();
        Task<ResponseBase<ClienteResponse>> LoginCliente(string cpf);
        Task<ResponseBase<ClienteResponse>> LoginClienteAnonimo();
        Task<ResponseBase<bool>> AlterarStatusCliente(int clienteId);
    }
}
