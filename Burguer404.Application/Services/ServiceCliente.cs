using AutoMapper;
using Burguer404.Application.Arguments.Cliente;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Entities.Cliente;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Domain.Ports.Services.Cliente;
using Burguer404.Domain.Validators;
using Burguer404.Domain.Validators.Cliente;

namespace Burguer404.Application.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryCliente _clienteRepository;

        public ServiceCliente(IMapper mapper, IRepositoryCliente clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseBase<ClienteResponse>> CadastrarCliente(ClienteRequest request)
        {
            var response = new ResponseBase<ClienteResponse>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarCliente.Validar_CadastroCliente_Request(request);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var entidade = _mapper.Map<ClienteRequest, ClienteEntity>(request);

            var clienteJaCadastrado = await _clienteRepository.ValidarCadastroCliente(entidade.Cpf, entidade.Email);
            if (clienteJaCadastrado)
            {
                response.Mensagem = "CPF já cadastrado como cliente!";
                return response;
            }

            entidade.Status = true;
            entidade = await _clienteRepository.CadastrarCliente(entidade);

            response.Resultado = [_mapper.Map<ClienteEntity, ClienteResponse>(entidade)];
            response.Sucesso = true;
            response.Mensagem = "Cliente cadastrado com sucesso!";

            return response;
        }

        public async Task<ResponseBase<ClienteResponse>> ListarClientes()
        {
            var response = new ResponseBase<ClienteResponse>();
            var clientes = await _clienteRepository.ListarClientes();

            var clientesResponse = new List<ClienteResponse>();
            Parallel.ForEach(clientes, cliente => { clientesResponse.Add(_mapper.Map<ClienteEntity, ClienteResponse>(cliente)); });

            response.Resultado = clientesResponse;
            response.Sucesso = true;
            response.Mensagem = "Listagem de clientes realizadas com sucesso!";

            return response;
        }

        public async Task<ResponseBase<ClienteResponse>> LoginCliente(string cpf)
        {
            var response = new ResponseBase<ClienteResponse>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarCliente.Validar_LoginCliente_Request(cpf);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            var cliente = await _clienteRepository.ObterClientePorCpf(cpf);

            validacoes = ValidarCliente.Validar_ExistenciaCliente(cliente, cpf);

            if (!validacoes.Sucesso)
            {
                response.Mensagem = validacoes.Mensagem;
                return response;
            }

            response.Resultado = [_mapper.Map<ClienteEntity, ClienteResponse>(cliente)];
            response.Sucesso = true;
            response.Mensagem = "Login realizado com sucesso!";

            return response;
        }

        public async Task<ResponseBase<bool>> AlterarStatusCliente(int clienteId)
        {
            var response = new ResponseBase<bool>();
            var validacoes = new ResponseBaseValidacoes();

            validacoes = ValidarCliente.Validar_AlterarStatusCliente_Request(clienteId);

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

            response.Sucesso = true;
            response.Mensagem = "Status do cliente alterado com sucesso!";

            return response;
        }

        public async Task<ResponseBase<ClienteResponse>> LoginClienteAnonimo()
        {
            var response = new ResponseBase<ClienteResponse>();

            var clienteAnonimo = await _clienteRepository.CadastrarClienteAnonimo();

            response.Resultado = [_mapper.Map<ClienteEntity, ClienteResponse>(clienteAnonimo)];
            response.Sucesso = true;
            response.Mensagem = "Cliente cadastrado com sucesso!";

            return response;
        }
    }
}
