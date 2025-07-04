using Burguer404.Application.Arguments.Cliente;
using Burguer404.Application.Controllers;
using Burguer404.Domain.Arguments.Base;
using Burguer404.Domain.Ports.Repositories.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Burguer404.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteHandler : Controller
    {
        private ClienteController _clienteController;

        public ClienteHandler(IRepositoryCliente clienteRepository)
        {
            _clienteController = new ClienteController(clienteRepository);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarCliente(ClienteRequest request)
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.CadastrarCliente(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response); 
            }
        }

        [HttpGet("listar")]
        public async Task<ActionResult> ListarClientes()
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.ListarClientes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("autenticar/cliente")]
        public async Task<ActionResult> LoginCliente(string cpf)
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.LoginCliente(cpf);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("autenticar/anonimo")]
        public async Task<ActionResult> LoginClienteAnonimo()
        {
            var response = new ResponseBase<ClienteResponse>();
            try
            {
                response = await _clienteController.LoginClienteAnonimo();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("alterar/status")]
        public async Task<ActionResult> AlterarStatusCliente(int clienteId)
        {
            var response = new ResponseBase<bool>();
            try
            {
                response = await _clienteController.AlterarStatusCliente(clienteId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                return BadRequest(response);
            }
        }

    }
}
