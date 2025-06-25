using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Application.UseCases.Cliente
{
    public sealed class AlterarStatusClienteUseCase
    {
        private readonly IRepositoryCliente _clienteRepository;

        public AlterarStatusClienteUseCase(IRepositoryCliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseBase<bool>> ExecuteAsync(int clienteId)
        {
            var response = new ResponseBase<bool>();
            var validacoes = ValidarCliente.Validar_AlterarStatusCliente_Request(clienteId);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var clienteAlterado = await _clienteRepository.AlterarStatusCliente(clienteId);
            if (!clienteAlterado)
            {
                response.Mensagem = "Cliente informado não encontrado!";
                return response;
            }

            response.Resultado = [clienteAlterado];
            response.Sucesso = true;
            response.Mensagem = "Status do cliente alterado com sucesso!";
            return response;
        }
    }
}
