using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Cliente;

namespace Burguer404.Application.Presenters
{
    public static class ClientePresenter
    {

        public static ResponseBase<ClienteResponse> ObterClienteResponse(ClienteEntity cliente) 
        {
            var response = new ResponseBase<ClienteResponse>() 
            {
                Sucesso = true,
                Mensagem = "Sucesso!",
                Resultado = [new ClienteResponse {
                    Id = cliente.Id,
                    Cpf = cliente.Cpf,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    PerfilClienteId = cliente.PerfilClienteId,
                    Status = cliente.Status
                }]
            };

            return response;
        }

        public static ResponseBase<ClienteResponse> ObterListaClienteResponse(List<ClienteEntity> clientes)
        {
            var listaClienteMap = new List<ClienteResponse>();

            foreach (var cliente in clientes)
            {
                var clienteMap = new ClienteResponse()
                {
                    Id = cliente.Id,
                    Cpf = cliente.Cpf,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    PerfilClienteId = cliente.PerfilClienteId,
                    Status = cliente.Status
                };

                listaClienteMap.Add(clienteMap);
            }

            var response = new ResponseBase<ClienteResponse>()
            {
                Sucesso = true,
                Mensagem = "Sucesso!",
                Resultado = listaClienteMap
            };

            return response;
        }

    }
}
